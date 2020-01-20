using EPiServer.ServiceLocation;
using MovieShop.Infrastructure.DependencyInjection;

namespace MovieShop.Foundation.Settings
{
    public class DependencyInjectionConfig : IDependencyInjectionConfig
    {
        public void Setup(IServiceConfigurationProvider container)
        {
            container.AddSingleton<ISettingService, SettingService>();
        }
    }
}