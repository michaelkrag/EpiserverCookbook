using CommonLib.Utilitys;
using System.Collections.Generic;

namespace MovieShop.Domain.Component
{
    public class MarketCurrency : ValueObject
    {
        public string MarketId { get; }
        public string CurrentCode { get; }
        public string MarketCurrencyCode { get; }
        public bool Validt { get; }

        private MarketCurrency(string marketId, string currentCode, string marketCurrencyCode)
        {
            MarketId = marketId;
            CurrentCode = currentCode;
            MarketCurrencyCode = marketCurrencyCode;
            Validt = true;
        }

        private MarketCurrency()
        {
            Validt = false;
        }

        private static MarketCurrency Empty => new MarketCurrency();

        public static MarketCurrency Create(string marketId, string currentCode)
        {
            if (string.IsNullOrEmpty(marketId) || string.IsNullOrEmpty(currentCode))
            {
                return Empty;
            }
            return new MarketCurrency(marketId, currentCode, $"{marketId}-{currentCode}");
        }

        public static MarketCurrency Create(string marketCurrencyCode)
        {
            var parts = marketCurrencyCode.Split('-');
            if (parts.Length != 2)
            {
                return Empty;
            }
            return new MarketCurrency(parts[0], parts[1], marketCurrencyCode);
        }

        public override string ToString()
        {
            return MarketCurrencyCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return MarketCurrencyCode;
            yield return Validt;
        }
    }
}