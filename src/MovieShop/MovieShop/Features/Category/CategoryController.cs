using EPiServer.Web.Mvc;
using MovieShop.Business.Factory;
using MovieShop.Domain.Commerce.Nodes;
using MovieShop.Features.Home;
using MovieShop.Foundation.Search;
using NLPLib.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MovieShop.Features.Category
{
    public class CategoryController : ContentController<GenreNode>
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ISearchEngine _searchEngine;

        public CategoryController(IViewModelFactory viewModelFactory, ISearchEngine searchEngine)
        {
            _viewModelFactory = viewModelFactory;
            _searchEngine = searchEngine;
        }

        public async Task<ActionResult> Index(GenreNode currentContent, HomePage currentPage)
        {
            var searchResult = _searchEngine.Query().MultiMatch("Back to the", new List<MatchField<ISearch>>()
                    {
                        new MatchField<ISearch>() { field = x => x.Title },
                        new MatchField<ISearch>() { field = x => x.Summery}
                    }).GetSearchHits<ISearch>();
            var categoryViewModel = new CategoryViewModel();
            categoryViewModel.SearchHits = searchResult;
            var viewModel = await _viewModelFactory.CreateCatalog(currentContent, currentPage, categoryViewModel);
            return View("~/Features/Category/CategoryView.cshtml", viewModel);
        }
    }
}