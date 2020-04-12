using EPiServer.Commerce;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using MovieShop.Domain.Settings.SettingsBlocke;
using MovieShop.Models.Blocks;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Foundation.Menu.Models.Settings
{
    [ContentType(DisplayName = "Menu settings", GUID = "B52DB53A-2AF0-4978-AF13-D92A4D3B69A0", Description = "Setting for menu")]
    public class MenuSettings : BaseBlock, IMenuSettings
    {
        [UIHint(UIHint.CatalogNode)]
        public virtual ContentReference MovieFolder { get; set; }
    }
}