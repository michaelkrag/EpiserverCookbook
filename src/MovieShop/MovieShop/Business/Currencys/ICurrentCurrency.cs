using Mediachase.Commerce;

namespace MovieShop.Business.Currencys
{
    public interface ICurrentCurrency
    {
        Currency GetCurrentCurrency();

        bool SetCurrentCurrency(string currencyCode);
    }
}