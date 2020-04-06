using EPiServer;
using EPiServer.Core;
using MovieShop.Features.Product;
using System.Collections.Generic;
using System.Linq;

namespace MovieShop.Foundation.Extensions
{
    public static class ContentLoaderExtensions
    {
        public static IEnumerable<TChild> GetAllChildren<TChild>(this IContentLoader contentLoader, ContentReference contentReference) where TChild : IContent
        {
            var usedIds = new HashSet<int>();
            var catalogRef = contentLoader.GetChildren<TChild>(contentReference).First();
            var nodeQueue = new Queue<TChild>(new List<TChild>() { catalogRef });
            while (nodeQueue.Any())
            {
                var contentData = nodeQueue.Dequeue();

                if (!usedIds.Contains(contentData.ContentLink.ID))
                {
                    usedIds.Add(contentData.ContentLink.ID);
                    var children = contentLoader.GetChildren<TChild>(contentData.ContentLink);
                    if (!(children is MovieProduct))
                    {
                        foreach (var child in children)
                        {
                            nodeQueue.Enqueue(child);
                        }
                    }
                    yield return contentData;
                }
            }
        }

        public static IEnumerable<TChild> GetAllChildren2<TChild>(this IContentLoader contentLoader, ContentReference contentReference) where TChild : IContent
        {
            var catalogRef = contentLoader.GetChildren<IContent>(contentReference);
            var nodeQueue = new Queue<IContent>(catalogRef);
            while (nodeQueue.Any())
            {
                var contentData = nodeQueue.Dequeue();
                var children = contentLoader.GetChildren<IContent>(contentData.ContentLink);
                foreach (var child in children)
                {
                    nodeQueue.Enqueue(child);
                }

                if (contentData is TChild content)
                {
                    yield return content;
                }
            }
        }
    }
}