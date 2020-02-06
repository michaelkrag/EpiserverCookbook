using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using MovieShop.Models.Pages;

namespace MovieShop.Foundation.Menu.Models
{
    [ContentType(DisplayName = "Menu grupe", GUID = "1F0F6389-C689-4C63-B704-CFA1DAF805A2")]
    [AvailableContentTypes(Availability.Specific, Include = new[] { typeof(SubMenuItem) })]
    public class MenuItem : BasePage
    {
        public virtual string MenuName { get; set; }
        public virtual ContentReference PageReference { get; set; }
    }
}