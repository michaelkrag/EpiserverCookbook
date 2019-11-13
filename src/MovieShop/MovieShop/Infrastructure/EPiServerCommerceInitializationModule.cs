using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Commerce.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace MovieShop.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class EPiServerCommerceInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            CatalogRouteHelper.MapDefaultHierarchialRouter(RouteTable.Routes, false);
            RouteTable.Routes.MapMvcAttributeRoutes();
        }

        public void Preload(string[] parameters)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}