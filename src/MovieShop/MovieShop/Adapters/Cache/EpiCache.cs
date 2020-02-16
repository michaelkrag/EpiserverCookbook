using CommonLib.Cache;
using CommonLib.Cache.Implementation;
using EPiServer.Framework.Cache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Adapters.Cache
{
    public class EpiCache : ICache
    {
        private readonly MsObjectCache _keyCache = new MsObjectCache();
        private readonly ISynchronizedObjectInstanceCache _syncronizedObjectInstanceCache;

        private readonly Dictionary<CacheDurationType, CacheTimeoutType> _typeConveter = new Dictionary<CacheDurationType, CacheTimeoutType>
        {
            {CacheDurationType.Absolute,   CacheTimeoutType.Absolute },
            {CacheDurationType.Infinite,   CacheTimeoutType.Undefined },
            {CacheDurationType.Sliding,   CacheTimeoutType.Sliding}
        };

        public EpiCache(ISynchronizedObjectInstanceCache syncronizedObjectInstanceCache)
        {
            _syncronizedObjectInstanceCache = syncronizedObjectInstanceCache;
        }

        public TObj Get<TObj>(string key)
        {
            return (TObj)GetObjet<object>(key);
        }

        public TObj GetObjet<TObj>(string key) where TObj : class
        {
            return _syncronizedObjectInstanceCache.Get<TObj>(key, ReadStrategy.Immediate);
        }

        public void Remove(string key)
        {
            try
            {
                _syncronizedObjectInstanceCache.RemoveRemote(key);
                _syncronizedObjectInstanceCache.RemoveLocal(key);
                _keyCache.Remove(key);
            }
            catch (Exception ex)
            {
            }
        }

        public void Set<TObj>(string key, TObj obj, TimeSpan timeSpan, CacheDurationType cacheDuration)
        {
            if (obj != null && !string.IsNullOrEmpty(key))
            {
                var cacheEvictionPolicy = new CacheEvictionPolicy(timeSpan, _typeConveter[cacheDuration]);
                _syncronizedObjectInstanceCache.Insert(key, obj, cacheEvictionPolicy);
                _keyCache.Set(key, DateTime.Now.Add(timeSpan), timeSpan, cacheDuration);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var entry in _keyCache)
            {
                if (_syncronizedObjectInstanceCache.TryGet(entry.Key, ReadStrategy.Immediate, out object instance))
                {
                    yield return new KeyValuePair<string, object>(entry.Key, instance);
                }
            }
        }
    }
}