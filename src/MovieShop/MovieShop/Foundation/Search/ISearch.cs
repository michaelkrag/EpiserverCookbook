using EPiServer.Core;
using NLPLib.Search.Attributes;
using System;
using System.Collections.Generic;
using MovieShop.Domain.Commerce.Products;

namespace MovieShop.Foundation.Search
{
    public interface ISearch
    {
        [Indexing]
        string Title { get; set; }

        [Indexing]
        string Overview { get; set; }

        ContentReference ContentLink { get; set; }
        DateTime ReleaseDate { get; set; }
        double VoteAverage { get; set; }
        int VoteCount { get; set; }
        double Popularity { get; set; }
        string Genres { get; set; }
        string OrginalLang { get; set; }
        string BelongsToCollection { get; set; }
        string SpokenLanguages { get; set; }
        bool Adult { get; set; }
        string OriginalTitle { get; set; }
        IList<Cast> Casts { get; set; }
        IList<Crew> Crews { get; set; }
        ContentReference Poster { get; }
    }
}