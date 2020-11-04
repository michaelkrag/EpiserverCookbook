using SuggestionApi.InformationRetrieval;
using SuggestionApi.Infrastructor.Document;
using SuggestionApi.Models.Indexing;
using SuggestionApi.NLP.Gram;
using SuggestionApi.NLP.Tokenizers;
using SuggestionApi.NLP.Vocabularys;
using System.Collections.Generic;

namespace SuggestionApi.Services
{
    public class IndexService
    {
        private readonly INGramRepository _iNGramRepository;
        private readonly ITokenizer _tokenizer;
        private readonly IVocabularyRepository _vocabularyRepository;
        private readonly IDocumentStoreRepository _documentStoreRepository;
        private readonly ISearchRespository _searchRespository;

        public IndexService(INGramRepository iNGramRepository, ITokenizer tokenizer, IVocabularyRepository vocabularyRepository, IDocumentStoreRepository documentStoreRepository, ISearchRespository searchRespository)
        {
            _iNGramRepository = iNGramRepository;
            _tokenizer = tokenizer;
            _vocabularyRepository = vocabularyRepository;
            _documentStoreRepository = documentStoreRepository;
            _searchRespository = searchRespository;
        }

        public int Indexing(string indexName, IEnumerable<Doc> docs)
        {
            var ngram = _iNGramRepository.Get(indexName, 1);
            var vocabulary = _vocabularyRepository.Get(indexName);
            var documentStore = _documentStoreRepository.Get(indexName);
            var reverseIndex = _searchRespository.Get(indexName, vocabulary);
            foreach (var doc in docs)
            {
                foreach (var field in doc.fields)
                {
                    foreach (var sentence in _tokenizer.Parse(field.value))
                    {
                        var sentenceId = documentStore.Insert(doc.id, sentence.Word);
                        var tokens = vocabulary.Add(sentence.Tokens);
                        //index sentence
                        ngram.Add(sentence.Tokens);
                        reverseIndex.Index(sentenceId.ToString(), tokens);
                    }
                }
            }
            _iNGramRepository.Set(indexName, ngram);
            _vocabularyRepository.Set(indexName, vocabulary);
            _searchRespository.Set(indexName, reverseIndex);
            return 0;
        }
    }
}