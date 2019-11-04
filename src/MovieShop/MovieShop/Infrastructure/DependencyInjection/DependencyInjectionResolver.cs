using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MovieShop.Infrastructure.DependencyInjection
{
    public class DependencyInjectionResolver : IDependencyResolver
    {
        private readonly IServiceLocator _serviceLocator;

        public DependencyInjectionResolver(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract)
            {
                return GetInterfaceService(serviceType);
            }
            return GetConcreteService(serviceType);
        }

        private object GetConcreteService(Type serviceType)
        {
            try
            {
                return _serviceLocator.GetInstance(serviceType);
            }
            catch (ActivationException ex)
            {
                throw new Exception("Can't create " + serviceType.Name + " ex:" + ex.InnerException.InnerException);
            }
        }

        private object GetInterfaceService(Type serviceType)
        {
            if (_serviceLocator.TryGetExistingInstance(serviceType, out object instance))
            {
                return instance;
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceLocator.GetAllInstances(serviceType).Cast<object>();
        }
    }
}