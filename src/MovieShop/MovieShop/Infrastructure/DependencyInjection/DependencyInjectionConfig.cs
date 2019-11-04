using EPiServer.ServiceLocation;
using MovieShop.Models;
using System;
using System.Linq;

namespace MovieShop.Infrastructure.DependencyInjection
{
    public class DependencyInjectionConfig
    {
        public static void Setup(IServiceConfigurationProvider container)
        {
            SetupFeatureModules(container);
        }

        private static void SetupFeatureModules(IServiceConfigurationProvider container)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(IDependencyInjectionConfig).IsAssignableFrom(p) && !p.IsInterface);

            foreach (var type in types)
            {
                var methodInfo = type.GetMethod(nameof(IDependencyInjectionConfig.Setup));
                var classInstance = Activator.CreateInstance(type, null);
                methodInfo.Invoke(classInstance, new object[] { container });
            }
        }
    }
}