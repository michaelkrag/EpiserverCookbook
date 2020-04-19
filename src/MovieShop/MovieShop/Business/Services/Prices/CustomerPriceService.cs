using CommonLib.Extensions;
using EPiServer.Commerce.Marketing;
using EPiServer.Core;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Pricing;
using MovieShop.Business.Currencys;
using MovieShop.Business.Extensions;
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
        private readonly ICurrentCurrency _currentCurrency;
        private readonly IPromotionEngine _promotionEngine;
        private readonly ReferenceConverter _referenceConverter;

        public CustomerPriceService(IPriceService priceService, ICurrentMarket currentMarket, ICurrentCurrency currentCurrency, IPromotionEngine promotionEngine, ReferenceConverter referenceConverter)
        {
            _priceService = priceService;
            _currentMarket = currentMarket;
            _currentCurrency = currentCurrency;
            _promotionEngine = promotionEngine;
            _referenceConverter = referenceConverter;
        }

        public IPriceValue GetPrice(string code)
        {
            return GetPrice(code, _currentMarket.GetCurrentMarket().MarketId, _currentCurrency.GetCurrentCurrency());
        }

        public IPriceValue GetPrice(string code, MarketId marketId, Currency currency)
        {
            return _priceService.GetPrices(marketId, DateTime.Now, new CatalogKey(code), new PriceFilter() { Currencies = currency.Yield() })
                                .GetLowesPrice()
                                .FirstOrDefault();
        }

        public IEnumerable<IPriceValue> GetPrices(IEnumerable<string> codes)
        {
            return GetPrices(codes, _currentMarket.GetCurrentMarket().MarketId, _currentCurrency.GetCurrentCurrency());
        }

        public IEnumerable<IPriceValue> GetPrices(IEnumerable<string> codes, MarketId marketId, Currency currency)
        {
            return _priceService.GetPrices(marketId, DateTime.Now, codes.Select(x => new CatalogKey(x)), new PriceFilter() { Currencies = currency.Yield() })
                                .GetLowesPrice();
        }

        public IEnumerable<DiscountedEntry> GetDiscountPrices(string code)
        {
            var reference = _referenceConverter.GetContentLink(code);
            return GetDiscountPrices(reference);
        }

        public IEnumerable<DiscountedEntry> GetDiscountPrices(IEnumerable<string> codes)
        {
            var reference = _referenceConverter.GetContentLinks(codes).Select(x=>x.Value);
            return GetDiscountPrices(reference);
        }

        public IEnumerable<DiscountedEntry> GetDiscountPrices(ContentReference contentReference)
        {
            var rewards = _promotionEngine.GetDiscountPrices(contentReference, _currentMarket.GetCurrentMarket(), _currentCurrency.GetCurrentCurrency()).ToList();
            return rewards;
        }

        public IEnumerable<DiscountedEntry> GetDiscountPrices(IEnumerable<ContentReference> contentReferences)
        {
            var rewards = _promotionEngine.GetDiscountPrices(contentReferences, _currentMarket.GetCurrentMarket(), _currentCurrency.GetCurrentCurrency()).ToList();
            return rewards;
        }
    }
}