using EPiServer.Core;
using EPiServer.DataAnnotations;
using MovieShop.Domain.Settings;
using MovieShop.Models.Pages;

namespace MovieShop.Features.Setting
{
    [ContentType(DisplayName = "Settings", GUID = "20ACCAD9-38CF-4A86-A5AC-503A3B1DC9BC", Description = "")]
    public class SettingsPage : BasePage, ISettingsPage
    {
        [AllowedTypes(new[] { typeof(ISettingBlock) })]
        public virtual ContentArea Settings { get; set; }
    }
}