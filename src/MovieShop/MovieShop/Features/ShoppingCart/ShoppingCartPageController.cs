using MediatR;
using MovieShop.Business.Factory;
using MovieShop.Controllers;
using MovieShop.Domain.MediaR;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MovieShop.Features.ShoppingCart
{
    public class ShoppingCartPageController : BasePageController<ShoppingCartPage>
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IMediator _mediator;

        public ShoppingCartPageController(IViewModelFactory viewModelFactory, IMediator mediator)
        {
            _viewModelFactory = viewModelFactory;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index(ShoppingCartPage currentPage)
        {
            var data = await _mediator.Send(CartContentRequest.Create());
            var viewModel = await _viewModelFactory.Create(currentPage, data);
            return View("~/Features/ShoppingCart/ShoppingCart.cshtml", viewModel);
        }
    }
}