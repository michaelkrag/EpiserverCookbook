using MovieShop.Business.Factory;
using MovieShop.Controllers;
using MovieShop.Models.ViewModels;
using System.Web.Mvc;

namespace MovieShop.Features.Front
{
    public class StartPageController : BasePageController<StartPage>
    {
        private readonly IViewModelFactory _viewModelFactory;

        public StartPageController(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ActionResult Index(StartPage currentPage)
        {
            PageViewModel<StartPage> viewModel = _viewModelFactory.Create(currentPage);
            return View(viewModel);
        }
    }
}