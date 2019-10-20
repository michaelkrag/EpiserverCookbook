using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;

namespace MovieShop.Features.Catalog.Models
{
    [CatalogContentType(DisplayName = "MovieNode", MetaClassName = "Movie_Node", GUID = "deca5c88-6e2a-4889-9454-9a129cc1b97f", Description = "")]
    public class MovieNode : NodeContent
    {
    }
}