using EPiServer.Commerce.Marketing;
using EPiServer.Core;
using Mediachase.Commerce.Pricing;
using MovieShop.Domain.MediaR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Extensions
{
    public static class DictonaryExtensions
    {
        public static string GetPrice(this Dictionary<string, IPriceValue> prices, string code)
        {
            if (prices.TryGetValue(code, out var price))
            {
                return price.UnitPrice.ToString();
            }
            return "-";
        }

        public static IEnumerable<DiscountView> GetDiscounts(this Dictionary<ContentReference, IList<DiscountPrice>> discounts, ContentReference content)
        {
            if (discounts.TryGetValue(content, out var discountPrices))
            {
                return discountPrices.Select(x => new DiscountView() { Price = x.Price.ToString(), Discription = x.Promotion.Name });
            }
            return Enumerable.Empty<DiscountView>();
        }
    }
}