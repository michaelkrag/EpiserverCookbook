using SuggestionApi.NLP.Vocabularys;

using System;
using System.Collections.Concurrent;

namespace SuggestionApi.NLP.TernaryTree
{
    public class TernarySearchFactory : ITernarySearchFactory
    {
        private static ConcurrentDictionary<string, ITernarySearch> TernarySearchCollection = new ConcurrentDictionary<string, ITernarySearch>();
        private readonly IVocabularyRepository _vocabularyRepository;

        public TernarySearchFactory(IVocabularyRepository vocabularyRepository)
        {
            _vocabularyRepository = vocabularyRepository;
        }

        public ITernarySearch Get(string index)
        {
            try
            {
                return TernarySearchCollection.GetOrAdd(index, x => Load(x));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ITernarySearch Load(string index)
        {
            var vocabulary = _vocabularyRepository.Get(index);
            var ternarySearch = new TernarySearch();
            foreach (var term in vocabulary.GetAll())
            {
                ternarySearch.Insert(term.Term, term.Occurs);
            }
            return ternarySearch;
        }
    }
}