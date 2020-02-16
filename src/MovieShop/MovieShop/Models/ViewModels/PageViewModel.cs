﻿using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Models.ViewModels
{
    public class PageViewModel<TPageData> : ILayoutViewModel where TPageData : PageData
    {
        public TPageData CurrentPage { get; }
        public string Title { get; set; }

        public PageViewModel(TPageData currentPage)
        {
            CurrentPage = currentPage;
        }
    }

    public class PageViewModel<TPage, TData> : PageViewModel<TPage> where TPage : PageData
    {
        public TData CurrentData { get; }

        public PageViewModel(TPage currentPage, TData currentData) : base(currentPage)
        {
            CurrentData = currentData;
        }
    }

    public class CatalogViewModel<TCatalogContent, TPage> : PageViewModel<TPage> where TPage : PageData where TCatalogContent : CatalogContentBase
    {
        public TCatalogContent CurrentContent { get; }

        public CatalogViewModel(TCatalogContent catalogContent, TPage currentPage) : base(currentPage)
        {
            CurrentContent = catalogContent;
        }
    }
}