using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using MovieShop.Business.Factory;
using MovieShop.Features.Catalog.Products;
using MovieShop.Features.Home;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MovieShop.Features.Catalog.Products
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
            return View("~/Features/Catalog/Products/MovieProductView.cshtml", viewModel);
        }
    }
}