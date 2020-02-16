using System;
using System.Threading.Tasks;

namespace CommonLib.Cache
{
    public interface ICacheAsync
    {
        Task<TObj> Get<TObj>(string key);

        Task Set(string key, object obj, TimeSpan timeSpan, CacheDurationType cacheDuration);

        Task Remove(string key);
    }
}