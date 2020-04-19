using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Extensions
{
    public static class PriceValueExtensions
    {
        public static IEnumerable<IPriceValue> GetLowesPrice(this IEnumerable<IPriceValue> priceValues)
        {
            return priceValues.GroupBy(x => x.CatalogKey).Select(x => x.OrderBy(y => y.UnitPrice.Amount)).Select(x => x.First());
        }
    }
}