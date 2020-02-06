using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using MovieShop.Models.Pages;

namespace MovieShop.Foundation.Menu.Models
{
    [ContentType(DisplayName = "Menu item", GUID = "7513845A-6174-4F0A-9D1F-2E6A84DFED91")]
    public class SubMenuItem : BasePage
    {
        public virtual string MenuName { get; set; }
        public virtual ContentReference PageReference { get; set; }
    }
}