using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Extensions
{
    public static class ContentAreaExtensions
    {
        public static IEnumerable<TType> GetBlockList<TType>(this ContentArea contentArea) where TType : IContentData
        {
            if (contentArea == null)
            {
                return Enumerable.Empty<TType>();
            }
            var items = contentArea.FilteredItems.Select(x => x.Get<TType>());
            return items.Where(x => x != null).ToList();
        }
    }
}