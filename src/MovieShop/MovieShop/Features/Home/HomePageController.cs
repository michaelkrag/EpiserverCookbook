using MovieShop.Business.Factory;
using MovieShop.Controllers;
using MovieShop.Models.ViewModels;
using System.Web.Mvc;

namespace MovieShop.Features.Home
{
    public class HomePageController : BasePageController<HomePage>
    {
        private readonly IViewModelFactory _viewModelFactory;

        public HomePageController(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ActionResult Index(HomePage currentPage)
        {
            var viewModel = _viewModelFactory.Create(currentPage);
            return View("~/Features/Home/HomePage.cshtml", viewModel);
        }
    }
}