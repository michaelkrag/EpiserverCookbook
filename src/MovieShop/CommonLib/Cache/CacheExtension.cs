using CommonLib.Cache.Implementation;
using CommonLib.Cache.Infrastructor;
using System;
using System.Threading.Tasks;

namespace CommonLib.Cache
{
    public static class CacheExtension
    {
        private static readonly MemoryCacheAside LockCache = new MemoryCacheAside(TimeOut.LockCacheTimeOut);

        public static T GetOrCreate<T>(this ICache cache, string key, Func<T> func, TimeSpan expiresIn, CacheDurationType cacheDuration, bool forceUpdate = false) where T : class
        {
            var rtnObj = cache.Get<T>(key);

            if (!rtnObj.IsNullOrEmpty() && forceUpdate == false)
            {
                return rtnObj;
            }

            object lockOn = LockCache.GetOrAdd(key, () => new object());
            using (new Lock(lockOn, TimeOut.LockTimeOut))
            {
                rtnObj = cache.Get<T>(key);
                if (rtnObj != null && forceUpdate == false)
                {
                    return rtnObj;
                }
                rtnObj = func();
                if (rtnObj != null)
                {
                    cache.Set(key, rtnObj, expiresIn, cacheDuration);
                }
                return rtnObj;
            }
        }

        public static async Task<T> GetOrCreateAsync<T>(this ICache cache, string key, Func<Task<T>> func, TimeSpan expiresIn, CacheDurationType cacheDuration, bool forceUpdate = false) where T : class
        {
            var rtnObj = cache.Get<T>(key);
            if (rtnObj != null && forceUpdate == false)
            {
                return rtnObj;
            }

            var lockOn = LockCache.GetOrAdd(key, () => new AsyncLock());

            using (await lockOn.LockAsync(TimeOut.LockTimeOut))
            {
                rtnObj = cache.Get<T>(key);
                if (rtnObj != null && forceUpdate == false)
                {
                    return rtnObj;
                }
                rtnObj = await func();

                cache.Set(key, rtnObj, expiresIn, cacheDuration);
                return rtnObj;
            }
        }

        private static bool IsNullOrEmpty<T>(this T obj)
        {
            if (obj == null)
            {
                return true;
            }
            /*   var objectType = obj.GetType();
               if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Option<>))
               {
                   var value = objectType.GetProperty("HasValue").GetValue(obj, null);
                   return !(bool)value;
               }*/
            return false;
        }

        public static bool TryGet<TObj>(this ICache cache, string key, out TObj obj) where TObj : class
        {
            obj = cache.Get<TObj>(key);
            if (obj == default(TObj))
            {
                return false;
            }
            return true;
        }
    }
}