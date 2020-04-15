using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using MovieShop.Infrastructure.DependencyInjection;

namespace MovieShop.Features.Market.Models
{
    public class DIConfig : IDependencyInjectionConfig
    {
        public void Setup(IServiceConfigurationProvider container)
        {
            container.AddSingleton<ICurrentMarket, CurrentMarket>();
        }
    }
}