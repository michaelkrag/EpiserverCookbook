using MovieShop.Business.Services.Blobstore;
using NLPLib.TernaryTree;
using NLPLib.Vocabularys;

namespace MovieShop.Foundation.Search
{
    public class TernaryTreeFactory : ITernaryTreeFactory
    {
        private readonly IBlobRepository _blobRepository;
        private readonly IVocabulary _vocabulary;

        public TernaryTreeFactory(IBlobRepository blobRepository, IVocabulary vocabulary)
        {
            _blobRepository = blobRepository;
            _vocabulary = vocabulary;
        }

        public ITernarySearch GenerateTree()
        {
            var ternarySearch = new TernarySearch();
            ternarySearch.Insert(_vocabulary);
            return ternarySearch;
        }
    }
}