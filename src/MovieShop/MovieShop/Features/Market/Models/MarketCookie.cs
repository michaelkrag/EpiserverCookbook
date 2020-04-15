using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.Market.Models
{
    public class MarketCookie
    {
        public const string CookieName = "Market";
        public string MarketId { get; set; }
    }
}