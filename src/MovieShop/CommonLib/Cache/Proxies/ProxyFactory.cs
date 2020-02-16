using Castle.DynamicProxy;
using System;

namespace CommonLib.Cache.Proxies
{
    public static class ProxyFactory
    {
        public static T Create<T>(T instance, ICache cache, TimeSpan timeOut, IProxyKeyHandler proxyKeyHandler)
        {
            return (T)new ProxyCache<T>(instance, cache, timeOut, proxyKeyHandler).GetTransparentProxy();
        }

        private static ProxyGenerator generator = new ProxyGenerator();

        public static T Create<T>(T instance, IAsyncInterceptor interceptor) where T : class
        {
            T proxy = generator.CreateInterfaceProxyWithTargetInterface<T>(instance, interceptor);
            return proxy;
        }
    }
}