using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.Catalog.Models
{
    [CatalogContentType(DisplayName = "MovieProduct", MetaClassName = "Movie_Product", GUID = "D96324E3-2090-4A48-AAFA-1B21A1DC2FA6", Description = "")]
    public class MovieProduct : ProductContent
    {
        public virtual string Title { get; }
        public virtual string Description { get; }
        //IList<string> Director { get; }
        //IList<string> Genres { get; }
        //IList<string> Starring { get; }
        //int Reating { get; }
    }
}