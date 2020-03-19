using EPiServer.DataAnnotations;
using MovieShop.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.Search
{
    [ContentType(DisplayName = "Search page", GUID = "AB3179D3-196D-4DC9-B8D2-909FBB1F9F3F", Description = "For generic search")]
    public class SearchPage : BasePage
    {
    }
}