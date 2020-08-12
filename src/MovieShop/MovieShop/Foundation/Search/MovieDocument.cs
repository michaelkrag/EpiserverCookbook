using EPiServer.Core;
using MovieShop.Domain.Commerce.Products;
using System;
using System.Collections.Generic;

namespace MovieShop.Foundation.Search
{
    public class MovieDocument
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public ContentReference ContentLink { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
        public double Popularity { get; set; }
        public string Genres { get; set; }
        public string OrginalLang { get; set; }
        public string BelongsToCollection { get; set; }
        public string SpokenLanguages { get; set; }
        public bool Adult { get; set; }
        public string OriginalTitle { get; set; }
        public List<Cast> Casts { get; set; }
        public List<Crew> Crews { get; set; }

        public ContentReference Poster { get; set; }
    }
}