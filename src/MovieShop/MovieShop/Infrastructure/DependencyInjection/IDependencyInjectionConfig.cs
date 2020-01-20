using EPiServer.ServiceLocation;

namespace MovieShop.Infrastructure.DependencyInjection
{
    public interface IDependencyInjectionConfig
    {
        void Setup(IServiceConfigurationProvider container);
    }
}