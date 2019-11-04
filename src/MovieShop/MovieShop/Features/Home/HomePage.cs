using EPiServer.Core;
using EPiServer.DataAnnotations;
using MovieShop.Models.Pages;

namespace MovieShop.Features.Home
{
    [ContentType(DisplayName = "StartPage", GUID = "2120230A-03F4-44EE-B47B-8D08F3FDE508", Description = "PageType used for the HomePage")]
    public class HomePage : BasePage
    {
        public virtual ContentArea Content { get; set; }
    }
}