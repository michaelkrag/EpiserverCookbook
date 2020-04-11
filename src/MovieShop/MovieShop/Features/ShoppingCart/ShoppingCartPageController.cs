using MovieShop.Business.Factory;
using MovieShop.Controllers;
using System.Web.Mvc;

namespace MovieShop.Features.ShoppingCart
{
    public class ShoppingCartPageController : BasePageController<ShoppingCartPage>
    {
        private readonly IViewModelFactory _viewModelFactory;

        public ShoppingCartPageController(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ActionResult Index(ShoppingCartPage currentPage)
        {
            var viewModel = _viewModelFactory.Create(currentPage);
            return View("~/Features/ShoppingCart/ShoppingCart.cshtml", viewModel);
        }
    }
}