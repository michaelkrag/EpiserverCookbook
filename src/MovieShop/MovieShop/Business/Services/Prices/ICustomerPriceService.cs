using Mediachase.Commerce.Pricing;

namespace MovieShop.Business.Services.Prices
{
    public interface ICustomerPriceService
    {
        IPriceValue GetPrice(string code);
    }
}