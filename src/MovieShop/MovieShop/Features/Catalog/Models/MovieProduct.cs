using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Features.Catalog.Models
{
    [CatalogContentType(DisplayName = "MovieProduct", MetaClassName = "Movie_Product", GUID = "D96324E3-2090-4A48-AAFA-1B21A1DC2FA6", Description = "")]
    public class MovieProduct : ProductContent
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

        //IList<string> Director { get; }
        //IList<string> Genres { get; }
        //IList<string> Starring { get; }
        //int Reating { get; }
    }
}