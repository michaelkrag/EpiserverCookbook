using CommonLib.Cache.Implementation;
using CommonLib.Cache.Infrastructor;
using System;
using System.Threading.Tasks;

namespace CommonLib.Cache
{
    public static class CacheAsyncExtension
    {
        private static readonly MemoryCacheAside LockCache = new MemoryCacheAside(TimeOut.LockCacheTimeOut);

        public static async Task<T> GetOrCreateAsync<T>(this ICacheAsync cache, string key, Func<Task<T>> func, TimeSpan expiresIn, CacheDurationType cacheDuration, bool forceUpdate = false) where T : class
        {
            var rtnObj = await cache.Get<T>(key);
            if (rtnObj != null && forceUpdate == false)
            {
                return rtnObj;
            }

            var lockOn = LockCache.GetOrAdd(key, () => new AsyncLock());

            using (await lockOn.LockAsync(TimeOut.AsyncLockTimeOut))
            {
                rtnObj = await cache.Get<T>(key);
                if (rtnObj != null && forceUpdate == false)
                {
                    return rtnObj;
                }
                rtnObj = await func();

                await cache.Set(key, rtnObj, expiresIn, cacheDuration);
                return rtnObj;
            }
        }

        public static async Task<T> GetOrCreateAsync<T>(this ICache cache, string key, Func<Task<T>> func, TimeSpan expiresIn, CacheDurationType cacheDuration, bool forceUpdate = false)
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
    }
}