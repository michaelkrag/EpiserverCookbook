using NLPLib.TernaryTree;

namespace MovieShop.Foundation.Search
{
    public interface ITernaryTreeFactory
    {
        ITernarySearch GenerateTree();
    }
}