using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace MovieShop.Foundation.Extensions
{
    public static class ContentAreaExtension
    {
        public static List<TType> GetBlockList<TType>(this ContentArea contentArea) where TType : IContentData
        {
            if (contentArea == null)
            {
                return new List<TType>();
            }

            var items = contentArea.FilteredItems.Select(x => x.Get<TType>());
            return items.Where(x => x != null).ToList();
        }
    }
}