using NLPLib.Search.Attributes;
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
    public class IrtRetSearch : IIrtRetSearch
    {
        private readonly ITokinizer _tokinizer;
        private InvertedIndex _invertedIndex = new InvertedIndex();
        private readonly IVocabulary _vocabulary;
        private readonly IDocumentStorage _documentStorage;
        private Dictionary<int, int> _documentNumberOfTerms = new Dictionary<int, int>();
        private int NumberOfDocuments = 0;
        public int NumberOfTerms = 0;

        public IrtRetSearch(IVocabulary vocabulary, IDocumentStorage documentStorage, ITokinizer tokinizer)
        {
            _vocabulary = vocabulary;
            _documentStorage = documentStorage;
            _tokinizer = tokinizer;
        }

        public int Indexing<TObj>(int documentId, TObj obj) where TObj : class
        {
            _documentStorage.Insert<TObj>(documentId, obj);
            NumberOfDocuments++;
            var numberOfTerms = 0;
            foreach (var member in typeof(TObj).GetProperties())
            {
                var attribute = member.GetCustomAttributes(typeof(IndexingAttribute), true).FirstOrDefault();
                if (attribute != null && member.PropertyType == typeof(string))
                {
                    var text = member.GetValue(obj, null) as string;
                    if (!string.IsNullOrEmpty(text))
                    {
                        foreach (var token in _tokinizer.GetTokens(text))
                        {
                            numberOfTerms++;
                            var wordId = _vocabulary.GetOrAddIndex(token.Term.ToLower());
                            _invertedIndex.Insert(wordId, documentId, token.Index);
                        }
                    }
                }
            }
            NumberOfTerms += numberOfTerms;
            _documentNumberOfTerms[documentId] = numberOfTerms;
            return documentId;
        }

        public TObj GetDocument<TObj>(int id) where TObj : class
        {
            return _documentStorage.Get<TObj>(id);
        }

        private IEnumerable<DocumentScore> SearchForTerm(int termId)
        {
            var documentHitList = _invertedIndex.Search(termId);
            var scoreCalculater = new Bm25(NumberOfDocuments, (double)NumberOfTerms / NumberOfDocuments);
            foreach (var docHit in documentHitList)
            {
                var score = scoreCalculater.Score(docHit.DocumentId, documentHitList.Count(), docHit.Offsets.Count(), _documentNumberOfTerms[docHit.DocumentId]);
                yield return new DocumentScore() { DocumentId = docHit.DocumentId, Score = score };
            }
        }

        public IEnumerable<SearchHit<TObj>> Search<TObj>(string str) where TObj : class
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

        public void Import(SearchExport searchExport)
        {
            _invertedIndex = searchExport.InvertedIndex;
            _documentNumberOfTerms = searchExport.DocumentNumberOfTerms;
            NumberOfDocuments = searchExport.NumberOfDocuments;
            NumberOfTerms = searchExport.NumberOfTerms;
        }

        public SearchExport Export()
        {
            return new SearchExport()
            {
                NumberOfTerms = NumberOfTerms,
                NumberOfDocuments = NumberOfDocuments,
                DocumentNumberOfTerms = _documentNumberOfTerms,
                InvertedIndex = _invertedIndex
            };
        }
    }
}