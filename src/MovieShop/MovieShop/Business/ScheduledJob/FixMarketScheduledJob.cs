using EPiServer;
using EPiServer.Core;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.Security;
using EPiServer.DataAbstraction;
using Mediachase.Commerce.Catalog;
using EPiServer.Commerce.Security;
using MovieShop.Foundation.Extensions;
using MovieShop.Domain.Commerce.Variants;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Markets;
using System.Collections.Generic;
using System;
using Mediachase.Commerce;
using CommonLib.Extensions;
using System.Linq;

namespace MovieShop.Business.ScheduledJob
{
    [ScheduledPlugIn(DisplayName = "FixMarketScheduledJob", GUID = "9A65E792-8288-4C92-82F2-0A66E659094F")]
    public class FixMarketScheduledJob : ScheduledJobBase
    {
        private readonly IContentLoader _contentLoader;
        private readonly ReferenceConverter _referenceConverter;
        private readonly IPriceDetailService _priceDetailService;
        private readonly IMarketService _marketService;
        private readonly IPriceService _priceService;

        public FixMarketScheduledJob(IContentLoader contentLoader, ReferenceConverter referenceConverter, IPriceDetailService priceDetailService, IMarketService marketService, IPriceService priceService)
        {
            _contentLoader = contentLoader;
            _referenceConverter = referenceConverter;
            _priceDetailService = priceDetailService;
            _marketService = marketService;
            _priceService = priceService;
        }

        public override string Execute()
        {
            var allMarkets = _marketService.GetAllMarkets();
            var added = 0;
            foreach (var variant in _contentLoader.GetAllChildren<MovieVariant>(_referenceConverter.GetRootLink()))
            {
                var newPrices = new List<PriceDetailValue>();
                var currentPrices = _priceDetailService.List(variant.ContentLink);
                //     var currentPrices2 = _priceService.GetCatalogEntryPrices(new CatalogKey(variant.Code));

                var markets = currentPrices?.Select(x => x.MarketId).Distinct().ToHashSet();

                foreach (var market in allMarkets)
                {
                    if (!markets.Contains(market.MarketId))
                    {
                        foreach (var currency in market.Currencies)
                        {
                            PriceDetailValue newPriceEntry = new PriceDetailValue();

                            newPriceEntry.CatalogKey = new CatalogKey(variant.Code);
                            newPriceEntry.MinQuantity = 0;
                            newPriceEntry.MarketId = market.MarketId;
                            newPriceEntry.UnitPrice = new Money(GetPrice(currency), currency);
                            newPriceEntry.ValidFrom = DateTime.Now.AddDays(-1);
                            newPriceEntry.ValidUntil = DateTime.Now.AddYears(20);
                            newPriceEntry.CustomerPricing = new CustomerPricing(0, "");
                            newPrices.Add(newPriceEntry);
                        }
                    }
                }

                if (newPrices.Any())
                {
                    _priceDetailService.Save(newPrices);
                    added += newPrices.Count;
                }
            }
            return $"Added {added} prices";
        }

        private static Random _random = new Random();

        public decimal GetPrice(Currency currency)
        {
            if (currency == Currency.DKK)
            {
                return _random.Next(119, 240);
            }
            if (currency == Currency.EUR)
            {
                return _random.Next(8, 11);
            }
            if (currency == Currency.USD)
            {
                return _random.Next(5, 9);
            }
            return _random.Next(5, 900);
        }
    }
}