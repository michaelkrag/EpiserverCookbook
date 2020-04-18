using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Services.Prices
{
    public class CustomerPriceService : ICustomerPriceService
    {
        private readonly IPriceService _priceService;
        private readonly ICurrentMarket _currentMarket;

        public CustomerPriceService(IPriceService priceService, ICurrentMarket currentMarket)
        {
            _priceService = priceService;
            _currentMarket = currentMarket;
        }

        public IPriceValue GetPrice(string code)
        {
            return GetPrice(code, _currentMarket.GetCurrentMarket().MarketId);
        }

        public IPriceValue GetPrice(string code, MarketId marketId)
        {
            return _priceService.GetPrices(marketId, DateTime.Now, new CatalogKey(code), new PriceFilter())
                                .OrderBy(x => x.UnitPrice.Amount).FirstOrDefault();
        }
    }
}