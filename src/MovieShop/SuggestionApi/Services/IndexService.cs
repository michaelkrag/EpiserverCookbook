using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NLPLib.Tokenizers;
using SuggestionApi.Models.Indexing;
using SuggestionApi.NLP.Vocabularys;
using SuggestionApi.NLP.Vocabularys.Repository;

namespace SuggestionApi.Services
{
    public class IndexService
    {
        private readonly IndexFactory _indexFactory;
        private readonly IVocabularyFileFactory _vocabularyFileFactory;
        private static HashSet<char> specialTokens = new HashSet<char>() { '-', '!', '?', '.', '\\', '(', ')', ':', ';', ',' };

        public IndexService(IndexFactory indexFactory, IVocabularyFileFactory vocabularyFileFactory)
        {
            _indexFactory = indexFactory;
            _vocabularyFileFactory = vocabularyFileFactory;
        }

        public int Indexing(string indexName, IEnumerable<Doc> docs)
        {
            var vocabulary = _vocabularyFileFactory.Get(indexName);
            using (var index = _indexFactory.OpenIndex(indexName))
            {
                foreach (var doc in docs)
                {
                    var docId = index.DocumentStore.InsertOrUpdate(doc);

                    foreach (var field in doc.fields)
                    {
                        var tokens = field.value.Tokens(specialTokens);
                        index.Vocabulary.InsertOrUpdate(tokens.Select(x => x.Term));
                    }
                }
            }
            return 0;
        }

        public IVocabulary GetVocabulary(string indexName)
        {
            using (var index = _indexFactory.OpenIndex(indexName))
            {
                return index.Vocabulary;
            }
        }
    }
}