using EPiServer.Web.Mvc;
using MovieShop.Business.Factory;
using MovieShop.Features.Home;
using System.Web.Mvc;

namespace MovieShop.Features.Product
{
    public class MovieProductController : ContentController<MovieProduct>
    {
        private readonly IViewModelFactory _viewModelFactory;

        public MovieProductController(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ActionResult Index(MovieProduct currentContent, HomePage currentPage)
        {
            var viewModel = _viewModelFactory.CreateCatalog(currentContent, currentPage);
            return View("~/Features/Product/MovieProductView.cshtml", viewModel);
        }
    }
}