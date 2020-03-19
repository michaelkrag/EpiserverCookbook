using MovieShop.Business.Factory;
using MovieShop.Controllers;
using MovieShop.Models.ViewModels;
using System.Web.Mvc;

namespace MovieShop.Features.Search
{
    public class SearchPageController : BasePageController<SearchPage>
    {
        private readonly IViewModelFactory _viewModelFactory;

        public SearchPageController(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ActionResult Index(SearchPage currentPage)
        {
            var viewModel = _viewModelFactory.Create(currentPage);
            return View("~/Features/Search/SearchPage.cshtml", viewModel);
        }
    }
}