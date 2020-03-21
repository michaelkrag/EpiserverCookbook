using EPiServer.Core;
using NLPLib.Search.Attributes;

namespace MovieShop.Foundation.Search
{
    public interface ISearch
    {
        [Indexing]
        string Title { get; set; }

        [Indexing]
        string Summery { get; set; }

        string PosterPath { get; set; }
        ContentReference ContentLink { get; set; }
    }
}