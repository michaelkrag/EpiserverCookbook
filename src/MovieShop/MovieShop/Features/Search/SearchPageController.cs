using MovieShop.Business.Factory;
using MovieShop.Controllers;
using MovieShop.Foundation.Search;
using NLPLib.Search;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MovieShop.Features.Search
{
    public class SearchPageController : BasePageController<SearchPage>
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ISearchEngine _searchEngine;

        public SearchPageController(IViewModelFactory viewModelFactory, ISearchEngine searchEngine)
        {
            _viewModelFactory = viewModelFactory;
            _searchEngine = searchEngine;
        }

        public async Task<ActionResult> Index(SearchPage currentPage, string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return View("~/Features/Search/SearchPage.cshtml", _viewModelFactory.Create(currentPage));
            }
            var searchResult = _searchEngine.Query().MultiMatch(q, new List<MatchField<ISearch>>()
                    {
                        new MatchField<ISearch>() { field = x => x.Title },
                        new MatchField<ISearch>() { field = x => x.Overview}
                    }).GetSearchHits<ISearch>();

            var result = new SearchResultData()
            {
                Query = q,
                SearcheResults = searchResult.Select(x => x.Document).ToList()
            };
            var viewModel = await _viewModelFactory.Create(currentPage, result);
            return View("~/Features/Search/SearchPageResult.cshtml", viewModel);
        }
    }
}