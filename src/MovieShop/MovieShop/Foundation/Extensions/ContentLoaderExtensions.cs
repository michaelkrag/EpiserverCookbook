using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using MovieShop.Domain.Commerce.Products;
using System.Collections.Generic;
using System.Linq;

namespace MovieShop.Foundation.Extensions
{
    public static class ContentLoaderExtensions
    {
        public static IEnumerable<TChild> GetAllChildren<TChild>(this IContentLoader contentLoader, ContentReference contentReference) where TChild : IContent
        {
            var usedIds = new HashSet<int>();
            var catalogRef = contentLoader.GetChildren<IContent>(contentReference).First();
            var nodeQueue = new Queue<IContent>(new List<IContent>() { catalogRef });
            while (nodeQueue.Any())
            {
                var contentData = nodeQueue.Dequeue();

                if (!usedIds.Contains(contentData.ContentLink.ID))
                {
                    usedIds.Add(contentData.ContentLink.ID);
                    if (!(contentData is ProductContent) && !(contentData is VariationContent))
                    {
                        var children = contentLoader.GetChildren<IContent>(contentData.ContentLink);
                        foreach (var child in children)
                        {
                            nodeQueue.Enqueue(child);
                        }
                    }
                    if (contentData is TChild content)
                    {
                        yield return content;
                    }
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

/*

  public virtual IEnumerable<T> GetEntriesRecursive<T>(ContentReference parentLink, CultureInfo defaultCulture) where T : EntryContentBase
  {
      foreach (var nodeContent in LoadChildrenBatched<NodeContent>(parentLink, defaultCulture))
      {
           foreach (var entry in GetEntriesRecursive<T>(nodeContent.ContentLink, defaultCulture))
          {
                 yield return entry;
         }
     }

     foreach (var entry in LoadChildrenBatched<T>(parentLink, defaultCulture))
     {
         yield return entry;
     }
 }

 private IEnumerable<T> LoadChildrenBatched<T>(ContentReference parentLink, CultureInfo defaultCulture) where T : IContent
 {
     var start = 0;

     while (true)
     {
         var batch = _contentLoader.GetChildren<T>(parentLink, defaultCulture, start, 50);
         if (!batch.Any())
         {
             yield break;
         }

         foreach (var content in batch)
         {
             // Don't include linked products to avoid including them multiple times when traversing the catalog
             if (!parentLink.CompareToIgnoreWorkID(content.ParentLink))
             {
                 continue;
             }

             yield return content;
         }
         start += 50;
     }
 }

     */