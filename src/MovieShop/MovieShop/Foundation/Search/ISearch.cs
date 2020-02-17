using EPiServer.Core;
using NLPLib.Search.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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