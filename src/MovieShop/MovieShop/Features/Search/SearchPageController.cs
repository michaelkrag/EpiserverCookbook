﻿using MovieShop.Business.Factory;
using MovieShop.Controllers;
using MovieShop.Foundation.Search;
using MovieShop.Models.ViewModels;
using NLPLib.Search;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MovieShop.Features.Search
{
    public class SearchPageController : BasePageController<SearchPage>
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IIrtRetSearch _irtRetSearch;

        public SearchPageController(IViewModelFactory viewModelFactory, IIrtRetSearch irtRetSearch)
        {
            _viewModelFactory = viewModelFactory;
            _irtRetSearch = irtRetSearch;
        }

        public ActionResult Index(SearchPage currentPage, string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return View("~/Features/Search/SearchPage.cshtml", _viewModelFactory.Create(currentPage));
            }
            var searchResult = _irtRetSearch.Search<ISearch>(q, 10);
            var result = new SearchResultData()
            {
                Query = q,
                SearcheResults = searchResult.Select(x => x.Document).ToList()
            };

            return View("~/Features/Search/SearchPageResult.cshtml", _viewModelFactory.Create(currentPage, result));
        }
    }
}