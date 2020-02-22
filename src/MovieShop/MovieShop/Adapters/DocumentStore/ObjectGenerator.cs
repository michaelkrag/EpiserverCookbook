using Castle.DynamicProxy;
using System;

namespace MovieShop.Adapters.DocumentStore
{
    public class ObjectGenerator
    {
        private static ProxyGenerator _proxyGenerator = new ProxyGenerator();

        public static object Generate<TInterface>(object obj34) where TInterface : class
        {
            // var obj = _proxyGenerator.CreateInterfaceProxyWithoutTarget<TInterface>(new Interceptor());
            var obj = _proxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(TInterface), new Interceptor());
            return obj;
        }
    }

    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Before target call {invocation.Method.Name}");
            try
            {
                //  invocation.Proceed();
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