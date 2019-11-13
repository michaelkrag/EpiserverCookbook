using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.Catalog.Models
{
    [CatalogContentType(DisplayName = "Genre Node", MetaClassName = "Genre_Node", GUID = "4904756D-460A-4AF2-8B10-1241BD5EB098", Description = "")]
    public class GenreNode : NodeContent
    {
    }
}