using NLPLib.Search.DocumentStores;
using NLPLib.Search.Index;
using NLPLib.Search.Models;
using NLPLib.Search.Scores;
using NLPLib.Tokenizers;
using NLPLib.Vocabularys;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NLPLib.Search
{
    public class IrtRetSearch
    {
        private readonly ITokinizer _tokinizer;
        private InvertedIndex _invertedIndex = new InvertedIndex();
        private readonly IVocabulary _vocabulary;
        private readonly IDocumentStorage _documentStorage;
        private Dictionary<int, int> _numberOfTerms = new Dictionary<int, int>();
        private int NumberOfDocuments = 0;
        public int NumberOfTerms = 0;

        public IrtRetSearch(IVocabulary vocabulary, IDocumentStorage documentStorage, ITokinizer tokinizer)
        {
            _vocabulary = vocabulary;
            _documentStorage = documentStorage;
            _tokinizer = tokinizer;
        }

        public int Indexing<TObj>(int documentId, TObj obj)
        {
            _documentStorage.Insert<TObj>(documentId, obj);
            NumberOfDocuments++;
            var numberOfTerms = 0;
            foreach (var member in typeof(TObj).GetProperties())
            {
                if (member.PropertyType == typeof(string))
                {
                    var text = member.GetValue(obj, null) as string;

                    foreach (var token in _tokinizer.GetTokens(text))
                    {
                        numberOfTerms++;
                        var wordId = _vocabulary.GetOrAddIndex(token.Term.ToLower());
                        _invertedIndex.Insert(wordId, documentId, token.Index);
                    }
                }
            }
            NumberOfTerms += numberOfTerms;
            _numberOfTerms[documentId] = numberOfTerms;
            return documentId;
        }

        public TObj GetDocument<TObj>(int id)
        {
            return _documentStorage.Get<TObj>(id);
        }

        private IEnumerable<DocumentScore> SearchForTerm(int termId)
        {
            var documentHitList = _invertedIndex.Search(termId);
            var scoreCalculater = new Bm25(NumberOfDocuments, (double)NumberOfTerms / NumberOfDocuments);
            foreach (var docHit in documentHitList)
            {
                var score = scoreCalculater.Score(docHit.DocumentId, documentHitList.Count(), docHit.Offsets.Count(), _numberOfTerms[docHit.DocumentId]);
                yield return new DocumentScore() { DocumentId = docHit.DocumentId, Score = score };
            }
        }

        public IEnumerable<SearchHit<TObj>> Search<TObj>(string str)
        {
            var resultContainer = new ConcurrentDictionary<int, double>();
            var terms = _tokinizer.GetTokens(str.ToLower());
            var termIds = terms.Select(x => _vocabulary.GetIndex(x.Term)).Where(x => x != -1);

            foreach (var termId in termIds)
            {
                var docScore = SearchForTerm(termId);
                foreach (var score in docScore)
                {
                    var docTempScore = resultContainer.GetOrAdd(score.DocumentId, 0);
                    var newScore = docTempScore + score.Score;
                    resultContainer.TryUpdate(score.DocumentId, newScore, docTempScore);
                }
            }
            return resultContainer.Select(x => new SearchHit<TObj>() { Score = x.Value, Document = _documentStorage.Get<TObj>(x.Key) });
        }
    }
}