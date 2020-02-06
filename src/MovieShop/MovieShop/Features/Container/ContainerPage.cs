using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using MovieShop.Features.Menu.Models;

namespace MovieShop.Features.Container
{
    [ContentType(DisplayName = "Folder", GUID = "48579E1F-46B2-4891-9E8B-BC81FA01D54E", Description = "A container page")]
    [AvailableContentTypes(Availability.Specific, Include = new[] { typeof(MenuPage) })]
    public class ContainerPage : PageData
    {
    }
}