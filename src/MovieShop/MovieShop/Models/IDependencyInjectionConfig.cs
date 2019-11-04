using EPiServer.ServiceLocation;

namespace MovieShop.Models
{
    public interface IDependencyInjectionConfig
    {
        void Setup(IServiceConfigurationProvider container);
    }
}