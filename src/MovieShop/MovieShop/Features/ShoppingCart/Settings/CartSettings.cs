using EPiServer.Core;
using EPiServer.DataAnnotations;
using MovieShop.Domain.Settings;
using MovieShop.Models.Blocks;

namespace MovieShop.Features.ShoppingCart.Settings
{
    [ContentType(DisplayName = "Cart settings", GUID = "76137BE6-F4C9-42F5-BB84-B330827337AE", Description = "Setting for cart")]
    public class CartSettings : BaseBlock, ISettingBlock
    {
        public virtual ContentReference ShoppingCart { get; set; }
    }
}