﻿using EPiServer;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Foundation.Extensions
{
    public static class ContentLoaderExtensions
    {
        public static IEnumerable<TChild> GetAllChildren<TChild>(this IContentLoader contentLoader, ContentReference contentReference) where TChild : IContent
        {
            var catalogRef = contentLoader.GetChildren<TChild>(contentReference).First();
            var nodeQueue = new Queue<TChild>(new List<TChild>() { catalogRef });
            while (nodeQueue.Any())
            {
                var contentData = nodeQueue.Dequeue();
                yield return contentData;

                var children = contentLoader.GetChildren<TChild>(contentData.ContentLink);
                foreach (var child in children)
                {
                    nodeQueue.Enqueue(child);
                }
            }
        }
    }
}