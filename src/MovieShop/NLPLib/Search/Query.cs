using NLPLib.Search.DocumentStores;
using NLPLib.Search.Extensions;
using NLPLib.Search.Models;
using NLPLib.Tokenizers;
using NLPLib.Vocabularys;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NLPLib.Search
{
    public class Query : IQuery, IFilter
    {
        private readonly SearchEngine _searchEngine;
        private readonly IVocabulary _vocabulary;
        private readonly ITokinizer _tokinizer;
        private readonly IDocumentStorage _documentStorage;
        private IEnumerable<DocumentScore> _queryDocumants;

        public Query(SearchEngine searchEngine, IVocabulary vocabulary, ITokinizer tokinizer, IDocumentStorage documentStorage)
        {
            _searchEngine = searchEngine;
            _vocabulary = vocabulary;
            _tokinizer = tokinizer;
            _documentStorage = documentStorage;
        }

        public IFilter MultiMatch<TObj>(string query, IEnumerable<MatchField<TObj>> fields)
        {
            var resultContainer = new ConcurrentDictionary<int, double>();
            var terms = _tokinizer.GetTokens(query.ToLower());
            var termIds = terms.Select(x => new { id = _vocabulary.GetIndex(x.Term), term = x.Term }).Where(x => x.id != -1);
            foreach (var termId in termIds)
            {
                foreach (var field in fields)
                {
                    var fieldName = field.field.GetExpressionName();
                    var docScore = _searchEngine.SearchForTerm(termId.id, fieldName);
                    foreach (var score in docScore)
                    {
                        var docTempScore = resultContainer.GetOrAdd(score.DocumentId, 0);
                        var newScore = docTempScore + (score.Score * field.Boost);
                        resultContainer.TryUpdate(score.DocumentId, newScore, docTempScore);
                    }
                }
            }
            _queryDocumants = resultContainer.OrderByDescending(x => x.Value).Select(x => new DocumentScore() { DocumentId = x.Key, Score = x.Value }).ToList();
            return this;
        }

        public IEnumerable<SearchHit<TObj>> GetSearchHits<TObj>(int take = 10, int skib = 0) where TObj : class
        {
            return _queryDocumants.Skip(skib).Take(take).Select(x => new SearchHit<TObj>() { Score = x.Score, Document = _documentStorage.Get<TObj>(x.DocumentId) });
        }

        public IFilter Filer<TObj>(Expression<Func<TObj, bool>> field)
        {
            throw new NotImplementedException();
        }
    }
}