using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Web;
using MovieShop.Foundation.Search;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Domain.Commerce.Products
{
    [CatalogContentType(DisplayName = "MovieProduct", MetaClassName = "Movie_Product", GUID = "D96324E3-2090-4A48-AAFA-1B21A1DC2FA6", Description = "")]
    public class MovieProduct : ProductContent, ISearch
    {
        [Display(Name = "Adult")]
        public virtual bool Adult { get; set; }

        [Display(Name = "Title")]
        public virtual string Title { get; set; }

        [Display(Name = "Original title")]
        public virtual string OriginalTitle { get; set; }

        [Display(Name = "Popularity")]
        public virtual double Popularity { get; set; }

        [Display(Name = "Imdb id")]
        public virtual string ImdbId { get; set; }

        [Display(Name = "TheMovieDb id")]
        public virtual string TheMovieDbId { get; set; }

        [Display(Name = "Summery")]
        [UIHint(UIHint.Textarea)]
        public virtual string Summery { get; set; }

        public virtual string PosterPath { get; set; }
        public virtual string BackdropPath { get; set; }

        public virtual DateTime ReleaseDate { get; set; }

        public virtual double VoteAverage { get; set; }
        public virtual int VoteCount { get; set; }
        //IList<string> Director { get; }
        //IList<string> Genres { get; }
        //IList<string> Starring { get; }
        //int Reating { get; }
    }
}