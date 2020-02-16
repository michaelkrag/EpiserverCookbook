using Castle.DynamicProxy;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLib.Cache.Proxies.Interceptors
{
    public class CacheInterceptor : AsyncInterceptorBase
    {
        private readonly ICache _cache;

        public TimeSpan _timeOut { get; }

        public CacheInterceptor(ICache cache, TimeSpan timeOut)
        {
            _cache = cache;
            _timeOut = timeOut;
        }

        //command
        protected override async Task InterceptAsync(IInvocation invocation, Func<IInvocation, Task> proceed)
        {
            try
            {
                // Cannot simply return the the task, as any exceptions would not be caught below.
                await proceed(invocation).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error calling {invocation.Method.Name}.", ex);
                throw;
            }
        }

        //query
        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, Func<IInvocation, Task<TResult>> proceed)
        {
            try
            {
                var key = CreateKey($"{invocation.Method.DeclaringType.Name}.{invocation.Method.Name}", invocation.Arguments);
                Func<Task<TResult>> func = () => proceed(invocation);
                var value = await _cache.GetOrCreateAsync(key, func, _timeOut, CacheDurationType.Absolute);
                return value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error calling {invocation.Method.Name}.", ex);
                throw;
            }
        }

        private string CreateKey(string name, object[] args)
        {
            if (args == null || args.Count() == 0)
            {
                return name;
            }
            unchecked// Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + name.GetHashCode();
                foreach (var obj in args)
                {
                    hash = hash * 23 + (obj?.GetHashCode() ?? 0);
                }
                return hash.ToString();
            }
        }
    }
}