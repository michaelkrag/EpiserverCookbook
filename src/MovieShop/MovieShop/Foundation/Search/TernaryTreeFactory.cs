using MovieShop.Business.Services.Blobstore;
using NLPLib.TernaryTree;
using NLPLib.Vocabularys;
using NLPLib.Vocabularys.Models;
using System.Collections.Generic;

namespace MovieShop.Foundation.Search
{
    public class TernaryTreeFactory : ITernaryTreeFactory
    {
        private readonly IBlobRepository _blobRepository;

        public TernaryTreeFactory(IBlobRepository blobRepository)
        {
            _blobRepository = blobRepository;
        }

        public ITernarySearch GenerateTree()
        {
            var vocabularyItems = _blobRepository.Load<List<VocabularyItem>>("Vocabulary");
            var ternarySearch = new TernarySearch();
            if (vocabularyItems != null)
            {
                var vocabulary = new Vocabulary();
                vocabulary.Import(vocabularyItems);
                ternarySearch.Insert(vocabulary);
            }
            return ternarySearch;
        }
    }
}