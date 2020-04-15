using CommonLib.Cookies;
using Mediachase.Commerce;
using Mediachase.Commerce.Markets;
using MovieShop.Features.Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.Market
{
    public class CurrentMarket : ICurrentMarket
    {
        private readonly ICookieRepository _cookieRepository;
        private readonly IMarketService _marketService;

        public CurrentMarket(ICookieRepository cookieRepository, IMarketService marketService)
        {
            _cookieRepository = cookieRepository;
            _marketService = marketService;
        }

        public IMarket GetCurrentMarket()
        {
            var marketCookie = _cookieRepository.Get<MarketCookie>(MarketCookie.CookieName);
            if (marketCookie != null)
            {
                var market = _marketService.GetMarket(marketCookie.MarketId);
                if (market != null)
                {
                    return market;
                }
            }
            return _marketService.GetAllMarkets().FirstOrDefault();
        }

        public void SetCurrentMarket(MarketId marketId)
        {
            var cookie = new MarketCookie() { MarketId = marketId.Value };
            _cookieRepository.Set(MarketCookie.CookieName, new TimeSpan(100, 0, 0, 0, 0), cookie);
        }
    }
}