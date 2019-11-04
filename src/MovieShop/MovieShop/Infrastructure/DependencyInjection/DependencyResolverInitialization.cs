using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using System.Web.Mvc;

namespace MovieShop.Infrastructure.DependencyInjection
{
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    [InitializableModule]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.ConfigurationComplete += (o, e) =>
            {
                DependencyInjectionConfig.Setup(context.Services);
            };
        }

        public void Initialize(InitializationEngine context)
        {
            DependencyResolver.SetResolver(new DependencyInjectionResolver(context.Locate.Advanced));
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}