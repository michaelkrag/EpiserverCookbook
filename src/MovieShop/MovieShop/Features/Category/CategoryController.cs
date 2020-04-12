using EPiServer.Web.Mvc;
using MovieShop.Business.Factory;
using MovieShop.Domain.Commerce.Nodes;
using MovieShop.Features.Home;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MovieShop.Features.Category
{
    public class CategoryController : ContentController<GenreNode>
    {
        private readonly IViewModelFactory _viewModelFactory;

        public CategoryController(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public async Task<ActionResult> Index(GenreNode currentContent, HomePage currentPage)
        {
            var viewModel = await _viewModelFactory.CreateCatalog(currentContent, currentPage);
            return View("~/Features/Category/CategoryView.cshtml", viewModel);
        }
    }
}