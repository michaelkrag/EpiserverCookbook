using System;
using System.Runtime.Caching;
using System.Threading;

namespace CommonLib.Cache.Implementation
{
    public class MemoryCacheAside
    {
        private readonly ObjectCache _cache = new MemoryCache("MemoryCacheAside");

        private readonly TimeSpan _timeOut;

        public MemoryCacheAside(TimeSpan timeSpan)
        {
            _timeOut = timeSpan;
        }

        public T GetOrAdd<T>(string key, Func<T> valueFactory)
        {
            var newValue = new Lazy<T>(valueFactory, LazyThreadSafetyMode.PublicationOnly);
            CacheItemPolicy policy = new CacheItemPolicy() { SlidingExpiration = _timeOut };
            var value = (Lazy<T>)_cache.AddOrGetExisting(key, newValue, policy);
            return (value ?? newValue).Value;
        }
    }
}