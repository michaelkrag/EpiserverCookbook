using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Api.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EpiserverCookBook.Infrastructure.Search.Extensions
{
    public static partial class FilterExtensions
    {
        private static MethodInfo FilterMatchCaseInsensitive = typeof(EPiServer.Find.Filters).GetMethod("MatchCaseInsensitive", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any
, new[] { typeof(string), typeof(string) }, null);

        public static ITypeSearch<TSource> FilterOn<TSource>(this ITypeSearch<TSource> typeSearch, Expression<Func<TSource, string>> expression, IEnumerable<string> filterItems)
        {
            if (filterItems != null && filterItems.Any())
            {
                var filter = typeSearch.Client.BuildFilter<TSource>();
                foreach (var item in filterItems)
                {
                    var itemValue = Expression.Constant(item);
                    var filerCommand = Expression.Call(null, FilterMatchCaseInsensitive, new Expression[] { expression.Body, itemValue });
                    filter = filter.Or(Expression.Lambda<Func<TSource, Filter>>(filerCommand, expression.Parameters));
                }
                return typeSearch.Filter(filter);
            }
            return typeSearch;
        }
    }
}