using System;
using System.Collections.Generic;

namespace SuggestionApi.NLP.Gram.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> func)
        {
            if (dictionary.TryGetValue(key, out TValue value))
            {
                return value;
            }
            dictionary[key] = func(key);
            return dictionary[key];
        }
    }
}