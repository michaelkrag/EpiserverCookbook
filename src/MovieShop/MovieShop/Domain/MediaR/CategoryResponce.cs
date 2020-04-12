using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.MediaR
{
    public class CategoryResponce
    {
        public IEnumerable<CategoryEntry> CategoryEntries { get; }

        public CategoryResponce(IEnumerable<CategoryEntry> categoryEntries)
        {
            CategoryEntries = categoryEntries;
        }

        public static CategoryResponce Create(IEnumerable<CategoryEntry> categoryEntries)
        {
            return new CategoryResponce(categoryEntries);
        }
    }

    public class CategoryEntry
    {
        public ContentReference Link { get; set; }
        public string Title { get; set; }
    }
}