using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;

namespace MovieShop.Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceConfigurationProviderExtensions
    {
        public static void Add(this IServiceConfigurationProvider container, Dictionary<Type, Type> typeDictionary)
        {
            if (typeDictionary == null)
            {
                return;
            }
            foreach (var entry in typeDictionary)
            {
                container.AddScoped(entry.Key, entry.Value);
            }
        }
    }
}