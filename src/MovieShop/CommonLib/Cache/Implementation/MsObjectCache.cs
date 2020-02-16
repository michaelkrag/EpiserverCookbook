using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CommonLib.Cache.Implementation
{
    public class MsObjectCache : ICache
    {
        private readonly ObjectCache _cache = MemoryCache.Default;

        /// <summary>
        /// Gets a object of type TObj from Ms ObjectCache
        /// </summary>
        /// <typeparam name="TObj">The object type to get from Object cache</typeparam>
        /// <param name="key">The key that the object is cache with</param>
        /// <returns>The object from cache or NULL</returns>
        public TObj Get<TObj>(string key)
        {
            return (TObj)_cache[key];
        }

        public void Set<TObj>(string key, TObj obj, TimeSpan timeSpan, CacheDurationType cacheDuration)
        {
            var cacheItemPolicy = CreateCacheItemPolicy(cacheDuration, timeSpan);
            _cache.Set(key, obj, cacheItemPolicy);
        }

        private CacheItemPolicy CreateCacheItemPolicy(CacheDurationType cacheDuration, TimeSpan timeSpan)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            switch (cacheDuration)
            {
                case CacheDurationType.Absolute:
                    policy.AbsoluteExpiration = DateTimeOffset.Now.Add(timeSpan);
                    break;

                case CacheDurationType.Sliding:
                    policy.SlidingExpiration = timeSpan;
                    break;
            }
            return policy;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var keyVal in _cache)
            {
                yield return keyVal;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}