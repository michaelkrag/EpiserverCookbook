using EPiServer.Commerce.Marketing;
using EPiServer.Core;
using Mediachase.Commerce.Pricing;
using System.Collections.Generic;

namespace MovieShop.Business.Services.Prices
{
    public interface ICustomerPriceService
    {
        IPriceValue GetPrice(string code);

        IEnumerable<IPriceValue> GetPrices(IEnumerable<string> codes);

        IEnumerable<DiscountedEntry> GetDiscountPrices(IEnumerable<string> codes);

        IEnumerable<DiscountedEntry> GetDiscountPrices(IEnumerable<ContentReference> contentReferences);
    }
}