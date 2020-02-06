using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using MovieShop.Foundation.Menu.Models;
using MovieShop.Models.Pages;

namespace MovieShop.Features.Menu.Models
{
    [ContentType(DisplayName = "Menu", GUID = "3CE58316-ED39-458C-9C7C-8B182513C4FF")]
    [AvailableContentTypes(Availability.Specific, Include = new[] { typeof(MenuItem) })]
    public class MenuPage : BasePage, IMenu
    {
    }
}