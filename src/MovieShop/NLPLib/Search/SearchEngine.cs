using NLPLib.Search.Attributes;
using NLPLib.Search.DocumentStores;
using NLPLib.Search.Extensions;
using NLPLib.Search.Index;
using NLPLib.Search.Index.Models;
using NLPLib.Search.Models;
using NLPLib.Search.Scores;
using NLPLib.Tokenizers;
using NLPLib.Vocabularys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NLPLib.Search
{
    public class SearchEngine : ISearchEngine
    {
        private int NumberOfDocuments = 0;
        public int NumberOfTerms = 0;
        private Dictionary<int, int> _documentNumberOfTerms = new Dictionary<int, int>();
        private FieldIndex _fieldIndex = new FieldIndex();
        private readonly IVocabulary _vocabulary;
        private readonly IDocumentStorage _documentStorage;
        private readonly ITokinizer _tokinizer;

        public SearchEngine(IVocabulary vocabulary, IDocumentStorage documentStorage, ITokinizer tokinizer)
        {
            _vocabulary = vocabulary;
            _documentStorage = documentStorage;
            _tokinizer = tokinizer;
        }

        public int Indexing<TObj>(int documentId, TObj document) where TObj : class
        {
            int numberOfTerms = 0;
            _documentStorage.Insert<TObj>(documentId, document);
            NumberOfDocuments++;
            var fields = typeof(TObj).GetAttributeProperties<IndexingAttribute>();
            foreach (var field in fields)
            {
                if (field.PropertyType == typeof(string))
                {
                    if (field.GetValue(document) is string text)
                    {
                        numberOfTerms += IndexingText(documentId, text, field.FullName());
                    }
                }
                else if (field.PropertyType == typeof(int))
                {
                    if (field.GetValue(document) is int number)
                    {
                        numberOfTerms += IndexingNumber(documentId, number, field.FullName());
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            NumberOfTerms += numberOfTerms;
            _documentNumberOfTerms[documentId] = numberOfTerms;
            return 0;
        }

        public TObj GetDocument<TObj>(int id) where TObj : class
        {
            return _documentStorage.Get<TObj>(id);
        }

        private int IndexingText(int documentId, string text, string fieldName)
        {
            var numberOfTokens = 0;
            var tokens = _tokinizer.GetTokens(text);
            var invertedIndex = _fieldIndex.GetIndexer(fieldName);
            foreach (var token in tokens)
            {
                var wordId = _vocabulary.GetOrAddIndex(token.Term.ToLower());
                var termInfo = new TermInformation() { TermIndex = token.Index, StartIndex = 0, StopIndex = 0 };
                invertedIndex.Insert(wordId, documentId, termInfo);
                numberOfTokens++;
            }
            return numberOfTokens;
        }

        private int IndexingNumber(int documentId, int number, string fieldName)
        {
            var wordId = _vocabulary.GetOrAddIndex(number.ToString().ToLower());
            var termInfo = new TermInformation() { TermIndex = 0, StartIndex = 0, StopIndex = 0 };
            var invertedIndex = _fieldIndex.GetIndexer(fieldName);
            invertedIndex.Insert(wordId, documentId, termInfo);
            return 1;
        }

        public IEnumerable<DocumentScore> SearchForTerm(int termId, InvertedIndex invertedIndex)
        {
            var documentHitList = invertedIndex.Search(termId);
            var scoreCalculater = new Bm25(NumberOfDocuments, (double)NumberOfTerms / NumberOfDocuments);
            foreach (var docHit in documentHitList)
            {
                var score = scoreCalculater.Score(docHit.DocumentId, documentHitList.Count(), docHit.Offsets.Count(), _documentNumberOfTerms[docHit.DocumentId]);
                yield return new DocumentScore() { DocumentId = docHit.DocumentId, Score = score };
            }
        }

        public IEnumerable<DocumentScore> SearchForTerm(int termId, string fieldName)
        {
            var invertedIndex = _fieldIndex.GetIndexer(fieldName);
            return SearchForTerm(termId, invertedIndex);
        }

        public IQuery Query()
        {
            return new Query(this, _vocabulary, _tokinizer, _documentStorage);
        }

        public void Import(SearchExport searchExport)
        {
            _fieldIndex = searchExport.FieldIndex;
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
                FieldIndex = _fieldIndex
            };
        }
    }

    public class MatchField<TObj>
    {
        public Expression<Func<TObj, string>> field { get; set; }
        public int Boost { get; set; } = 1;
    }
}