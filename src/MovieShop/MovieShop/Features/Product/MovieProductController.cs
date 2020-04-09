using EPiServer.Web.Mvc;
using MediatR;
using MovieShop.Business.Factory;
using MovieShop.Domain.Commerce.Products;
using MovieShop.Domain.MediaR;
using MovieShop.Features.Home;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MovieShop.Features.Product
{
    public class MovieProductController : ContentController<MovieProduct>
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IMediator _mediator;

        public MovieProductController(IViewModelFactory viewModelFactory, IMediator mediator)
        {
            _viewModelFactory = viewModelFactory;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index(MovieProduct currentContent, HomePage currentPage, string code)
        {
            var variants = await _mediator.Send(VariantsRequest.Create(currentContent.ContentLink, code));
            var viewModel = _viewModelFactory.CreateCatalog(currentContent, currentPage, variants);
            return View("~/Features/Product/MovieProductView.cshtml", viewModel);
        }
    }
}