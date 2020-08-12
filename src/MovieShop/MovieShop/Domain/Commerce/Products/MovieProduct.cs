using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using MovieShop.Foundation.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.UI.WebControls;

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

        [Display(Name = "Overview")]
        [UIHint(UIHint.Textarea)]
        public virtual string Overview { get; set; }

        public virtual DateTime ReleaseDate { get; set; }
        public virtual double VoteAverage { get; set; }
        public virtual int VoteCount { get; set; } /**/
        public virtual string Genres { get; set; }
        public virtual string OrginalLang { get; set; }
        public virtual string BelongsToCollection { get; set; }
        public virtual string SpokenLanguages { get; set; }

        [BackingType(typeof(CastProperty))]
        public virtual IList<Cast> Casts { get; set; }

        [BackingType(typeof(CrewProperty))]
        public virtual IList<Crew> Crews { get; set; }

        public ContentReference Poster => MoviePoster();

        public ContentReference MoviePoster()
        {
            var poster = CommerceMediaCollection.Where(x => x.GroupName == "Poster").FirstOrDefault();
            if (poster != null)
            {
                return poster.AssetLink;
            }
            return CommerceMediaCollection.FirstOrDefault()?.AssetLink ?? ContentReference.EmptyReference;
        }
    }
}

/*
adult
Title
OrgiginalTilte
popularity
imdbId =
id = 3224
Overview
posterPath
releaseDate
voteaverage
Genres = Comedy
orginalLang = en
BelongsToCollection : name/ postPath/ backdropPath
SpokenLanguages

ProductionCompanies

cast { character , name }
Crew {name, job}

 */