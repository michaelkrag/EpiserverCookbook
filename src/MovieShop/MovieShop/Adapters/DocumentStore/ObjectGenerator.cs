using Castle.DynamicProxy;
using System;

namespace MovieShop.Adapters.DocumentStore
{
    public class ObjectGenerator
    {
        private static ProxyGenerator _proxyGenerator = new ProxyGenerator();

        private static TInterface Generate<TInterface>() where TInterface : class
        {
            return _proxyGenerator.CreateInterfaceProxyWithoutTarget<TInterface>(new Interceptor());
        }
    }

    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Before target call {invocation.Method.Name}");
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Target exception {ex.Message}");
                throw;
            }
            finally
            {
                Console.WriteLine($"After target call {invocation.Method.Name}");
            }
        }
    }
}