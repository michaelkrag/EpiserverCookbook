using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using MovieShop.Business.Currencys;
using MovieShop.Infrastructure.DependencyInjection;

namespace MovieShop.Features.Market
{
    public class DIConfig : IDependencyInjectionConfig
    {
        public void Setup(IServiceConfigurationProvider container)
        {
            container.AddSingleton<ICurrentMarket, CurrentMarket>();
            container.AddSingleton<ICurrentCurrency, CurrentCurrency>();
        }
    }
}