using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Domain.Commerce.Variants
{
    [CatalogContentType(DisplayName = "MovieVariation", MetaClassName = "Movie_Variation", GUID = "642C1551-E9AF-4DAF-8D11-3E0DA2F30E1D", Description = "")]
    public class MovieVariant : VariationContent
    {
        [Display(Name = "Title")]
        public virtual string Title { get; set; }

        [Display(Name = "Media types")]
        public virtual string MediaTypes { get; set; }

        [Display(Name = "Is primary")]
        public virtual bool IsPrimary { get; set; }
    }
}