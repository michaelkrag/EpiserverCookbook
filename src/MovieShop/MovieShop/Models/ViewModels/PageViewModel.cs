using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Models.ViewModels
{
    public class PageViewModel<TPageData> : ILayoutViewModel, IMenuViewModel where TPageData : PageData
    {
        public TPageData CurrentPage { get; }
        public string Title { get; set; }
        public string CartApiUrl { get; set; }

        public string RecommendTitle { get; set; } = "For you";

        public IEnumerable<MenuItem> Recommend { get; set; } = new List<MenuItem>();

        public string CategoriesTitle { get; set; } = "Categories";

        public IEnumerable<MenuItem> Categories { get; set; } = new List<MenuItem>();

        public string CartUrl { get; set; }

        public IEnumerable<SelectEntry> Markets { get; set; } = new List<SelectEntry>();

        public bool ShowRecommendTitle()
        {
            return Recommend.Any();
        }

        public bool ShowCategoriesTitle()
        {
            return Recommend.Any() && Categories.Any();
        }

        public PageViewModel(TPageData currentPage, string name)
        {
            CurrentPage = currentPage;
            Title = name;
        }
    }

    public class PageViewModel<TPage, TData> : PageViewModel<TPage> where TPage : PageData
    {
        public TData CurrentData { get; }

        public PageViewModel(TPage currentPage, TData currentData) : base(currentPage, currentPage.Name)
        {
            CurrentData = currentData;
        }
    }

    public class CatalogViewModel<TCatalogContent, TPage> : PageViewModel<TPage> where TPage : PageData
                                                                                 where TCatalogContent : CatalogContentBase
    {
        public TCatalogContent CurrentContent { get; }

        public CatalogViewModel(TCatalogContent catalogContent, TPage currentPage) : base(currentPage, catalogContent.Name)
        {
            CurrentContent = catalogContent;
        }
    }

    public class CatalogViewModel<TCatalogContent, TPage, TData> : CatalogViewModel<TCatalogContent, TPage> where TPage : PageData
                                                                                                            where TCatalogContent : CatalogContentBase
    {
        public TData CurrentData { get; }

        public CatalogViewModel(TCatalogContent catalogContent, TPage currentPage, TData data) : base(catalogContent, currentPage)
        {
            CurrentData = data;
        }
    }
}