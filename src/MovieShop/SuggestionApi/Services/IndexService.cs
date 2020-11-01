using SuggestionApi.Models.Indexing;
using SuggestionApi.NLP.Gram;
using SuggestionApi.NLP.Tokenizers;
using SuggestionApi.NLP.Vocabularys;
using SuggestionApi.NLP.Vocabularys.Repository;
using System.Collections.Generic;

namespace SuggestionApi.Services
{
    public class IndexService
    {
        private readonly IndexFactory _indexFactory;
        private readonly IVocabularyFileFactory _vocabularyFileFactory;
        private readonly INGramRepository _iNGramRepository;
        private readonly ITokenizer _tokenizer;

        public IndexService(IndexFactory indexFactory, IVocabularyFileFactory vocabularyFileFactory, INGramRepository iNGramRepository, ITokenizer tokenizer)
        {
            _indexFactory = indexFactory;
            _vocabularyFileFactory = vocabularyFileFactory;
            _iNGramRepository = iNGramRepository;
            _tokenizer = tokenizer;
        }

        public int Indexing(string indexName, IEnumerable<Doc> docs)
        {
            var vocabulary = _vocabularyFileFactory.Get(indexName);
            using (var index = _indexFactory.OpenIndex(indexName))
            {
                var ngram = _iNGramRepository.Get(indexName, 1);
                foreach (var doc in docs)
                {
                    foreach (var field in doc.fields)
                    {
                        foreach (var sentence in _tokenizer.Parse(field.value))
                        {
                            //index sentence
                            ngram.Add(sentence.Tokens);
                            index.Vocabulary.InsertOrUpdate(sentence.Tokens);
                        }
                    }
                }
                _iNGramRepository.Set(indexName, ngram);
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