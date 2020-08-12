using SuggestionApi.NLP.Vocabularys.Repository;
using System;
using System.Collections.Concurrent;

namespace SuggestionApi.NLP.TernaryTree
{
    public class TernarySearchFactory : ITernarySearchFactory
    {
        private static ConcurrentDictionary<string, ITernarySearch> TernarySearchCollection = new ConcurrentDictionary<string, ITernarySearch>();
        private readonly IVocabularyFileFactory _vocabularyFileFactory;

        public TernarySearchFactory(IVocabularyFileFactory vocabularyFileFactory)
        {
            _vocabularyFileFactory = vocabularyFileFactory;
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
            var vocabulary = _vocabularyFileFactory.Get(index);
            var ternarySearch = new TernarySearch();
            foreach (var term in vocabulary.GetAll())
            {
                ternarySearch.Insert(term.Term, term.Occurs);
            }
            return ternarySearch;
        }
    }
}