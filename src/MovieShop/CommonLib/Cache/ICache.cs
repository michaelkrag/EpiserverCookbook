using System;
using System.Collections.Generic;

namespace CommonLib.Cache
{
    public interface ICache : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// Gets a object of type TObj from cache
        /// </summary>
        /// <typeparam name="TObj">The object type to get from Object cache</typeparam>
        /// <param name="key">The key that the object is cache with</param>
        /// <returns>The object from cache or NULL</returns>
        TObj Get<TObj>(string key);

        /// <summary>
        /// Insert a object into cache
        /// </summary>
        /// <typeparam name="TObj"></typeparam>
        /// <param name="key">The kay that the object will be cache with</param>
        /// <param name="obj">The object that will be cache</param>
        /// <param name="timeSpan">How long time the object will be cache before it is injected from cache</param>
        /// <param name="cacheDuration">How the object will be cache</param>
        void Set<TObj>(string key, TObj obj, TimeSpan timeSpan, CacheDurationType cacheDuration);

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}