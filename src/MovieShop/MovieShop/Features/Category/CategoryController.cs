using EPiServer.Web.Mvc;
using MovieShop.Business.Factory;
using MovieShop.Domain.Commerce.Nodes;
using MovieShop.Features.Home;
using MovieShop.Foundation.Search;
using NLPLib.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using MovieShop.Foundation.MovieSearches;

namespace MovieShop.Features.Category
{
    public class CategoryController : ContentController<GenreNode>
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IMovieSearch _movieSearch;

        public CategoryController(IViewModelFactory viewModelFactory, IMovieSearch movieSearch)
        {
            _viewModelFactory = viewModelFactory;
            _movieSearch = movieSearch;
        }

        public async Task<ActionResult> Index(GenreNode currentContent, HomePage currentPage)
        {
            var image = currentContent.CommerceMediaCollection.FirstOrDefault(x => x.GroupName == "Default")?.AssetLink;

            var movies = _movieSearch.SearchByGenre(currentContent.Name);
            var categoryViewModel = new CategoryViewModel();
            categoryViewModel.SearchHits = movies;
            var viewModel = await _viewModelFactory.CreateCatalog(currentContent, currentPage, categoryViewModel);
            return View("~/Features/Category/CategoryView.cshtml", viewModel);
        }
    }
}