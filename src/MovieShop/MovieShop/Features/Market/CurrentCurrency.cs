using CommonLib.Cookies;
using Mediachase.Commerce;
using MovieShop.Business.Currencys;
using MovieShop.Features.Market.Models;
using System;
using System.Linq;

namespace MovieShop.Features.Market
{
    public class CurrentCurrency : ICurrentCurrency
    {
        private ICookieRepository _cookieRepository;
        private readonly ICurrentMarket _currentMarket;

        public CurrentCurrency(ICookieRepository cookieRepository, ICurrentMarket currentMarket)
        {
            _cookieRepository = cookieRepository;
            _currentMarket = currentMarket;
        }

        public Currency GetCurrentCurrency()
        {
            var currencyCookie = _cookieRepository.Get<CurrencyCookie>(CurrencyCookie.CookieName);

            var currentMaket = _currentMarket.GetCurrentMarket();
            var currency = currentMaket.Currencies.FirstOrDefault(x => x.CurrencyCode == currencyCookie.CurrencytId);
            if (currency != null)
            {
                return currency;
            }

            return currentMaket.DefaultCurrency;
        }

        public bool SetCurrentCurrency(string currencyCode)
        {
            var currentMaket = _currentMarket.GetCurrentMarket();
            var currency = currentMaket.Currencies.FirstOrDefault(x => x.CurrencyCode == currencyCode);
            if (currency != null)
            {
                var currencyCookie = _cookieRepository.Set<CurrencyCookie>(CurrencyCookie.CookieName, new TimeSpan(100, 0, 0, 0), new CurrencyCookie() { CurrencytId = currencyCode });
                return true;
            }
            return false;
        }
    }
}