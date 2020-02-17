using EPiServer.ServiceLocation;
using MovieShop.Business.Services.Blobstore;
using MovieShop.Business.Services.Search;
using CommonLib.Extensions;
using System;
using System.Linq;
using MovieShop.Infrastructure.DependencyInjection.Extensions;
using MovieShop.Foundation.Search;
using NLPLib.TernaryTree;

namespace MovieShop.Infrastructure.DependencyInjection
{
    public class DependencyInjectionConfig
    {
        public static void Setup(IServiceConfigurationProvider container)
        {
            SetupDefaultConvention(container);
            SetupFeatureModules(container);
            SetupExternal(container);
        }

        private static void SetupExternal(IServiceConfigurationProvider container)
        {
            container.AddTransient<IBlobFilenameRepository, BlobFilenameRepository>();

            container.AddTransient<IBlobRepository, BlobRepository>();
            container.AddSingleton<ITernaryTreeFactory, TernaryTreeFactory>();
            container.AddSingleton<ITernarySearch>(x => x.GetInstance<ITernaryTreeFactory>().GenerateTree());
        }

        public static void SetupDefaultConvention(IServiceConfigurationProvider container)
        {
            container.Add(AssemblyScanner.ForAssamby<DependencyResolverInitialization>().GetInterfaceWithDefaultConventions());
        }

        private static void SetupFeatureModules(IServiceConfigurationProvider container)
        {
            try
            {
                var types = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("MovieShop")).SelectMany(s => s.GetTypes()).Where(p => typeof(IDependencyInjectionConfig).IsAssignableFrom(p) && !p.IsInterface);
                foreach (var type in types)
                {
                    var methodInfo = type.GetMethod(nameof(IDependencyInjectionConfig.Setup));
                    var classInstance = Activator.CreateInstance(type, null);
                    methodInfo.Invoke(classInstance, new object[] { container });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}