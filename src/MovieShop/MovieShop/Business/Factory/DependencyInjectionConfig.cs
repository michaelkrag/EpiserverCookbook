using EPiServer.ServiceLocation;
using MovieShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Factory
{
    public class DependencyInjectionConfig : IDependencyInjectionConfig
    {
        public void Setup(IServiceConfigurationProvider container)
        {
            container.AddSingleton<IViewModelFactory, ViewModelFactory>();
        }
    }
}