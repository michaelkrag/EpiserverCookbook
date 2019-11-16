using MovieShop.Business.Services.Blobstore;
using MovieShop.Business.Services.Search.Models;
using NLPLib.Tools.Wordbook;

namespace MovieShop.Business.Services.Search
{
    public class TernaryTreeFactory : ITernaryTreeFactory
    {
        private readonly IBlobRepository _blobRepository;

        public TernaryTreeFactory(IBlobRepository blobRepository)
        {
            _blobRepository = blobRepository;
        }

        public TernaryTreeService GenerateTree()
        {
            var vocabulary = _blobRepository.Load<Vocabulary>("Vocabulary");
            var ternaryTreeService = new TernaryTreeService();
            if (vocabulary != null)
            {
                foreach (var entry in vocabulary)
                {
                    ternaryTreeService.Add(entry.Key, new TernaryTreeModel() { Word = entry.Key, WordCount = entry.Value });
                }
            }
            return ternaryTreeService;
        }
    }
}