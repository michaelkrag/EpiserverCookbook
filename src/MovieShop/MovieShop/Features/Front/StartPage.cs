using EPiServer.DataAnnotations;
using MovieShop.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.Front
{
    [ContentType(DisplayName = "StartPage", GUID = "2120230A-03F4-44EE-B47B-8D08F3FDE508", Description = "PageType used for the HomePage")]
    public class StartPage : BasePage
    {
    }
}