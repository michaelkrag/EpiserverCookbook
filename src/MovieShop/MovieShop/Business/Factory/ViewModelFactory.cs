using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using MediatR;
using MovieShop.Domain.MediaR;
using MovieShop.Domain.Settings.SettingsBlocke;
using MovieShop.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Business.Factory
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IMediator _mediator;
        private readonly IMenuSettings _menuSettings;

        public ViewModelFactory(IMediator mediator, IMenuSettings menuSettings)
        {
            _mediator = mediator;
            _menuSettings = menuSettings;
        }

        private async Task FullOutModel<TModel, TPage>(TModel viewModel) where TModel : PageViewModel<TPage> where TPage : PageData
        {
            viewModel.CartApiUrl = "/cart";
            viewModel.CartUrl = "/en/shooping-cart/";

            var bugerMenu = await _mediator.Send(CategoryRequest.Create(_menuSettings.MovieFolder));
            viewModel.Categories = bugerMenu.CategoryEntries.Select(x => new MenuItem() { Link = x.Link, Title = x.Title });
        }

        public async Task<PageViewModel<TPage>> Create<TPage>(TPage currentPage) where TPage : PageData
        {
            var viewModel = new PageViewModel<TPage>(currentPage, currentPage.Name);
            await FullOutModel<PageViewModel<TPage>, TPage>(viewModel);
            return viewModel;
        }

        public async Task<PageViewModel<TPage, TData>> Create<TPage, TData>(TPage currentPage, TData CurrentData) where TPage : PageData
        {
            var viewModel = new PageViewModel<TPage, TData>(currentPage, CurrentData);
            await FullOutModel<PageViewModel<TPage>, TPage>(viewModel);
            return viewModel;
        }

        public async Task<CatalogViewModel<TCatalogContent, TPage>> CreateCatalog<TCatalogContent, TPage>(TCatalogContent currentContent, TPage currentPage) where TCatalogContent : CatalogContentBase where TPage : PageData
        {
            var catalogViewModel = new CatalogViewModel<TCatalogContent, TPage>(currentContent, currentPage);
            await FullOutModel<PageViewModel<TPage>, TPage>(catalogViewModel);
            return catalogViewModel;
        }

        public async Task<CatalogViewModel<TCatalogContent, TPage, TData>> CreateCatalog<TCatalogContent, TPage, TData>(TCatalogContent currentContent, TPage currentPage, TData data) where TCatalogContent : CatalogContentBase where TPage : PageData
        {
            var catalogViewModel = new CatalogViewModel<TCatalogContent, TPage, TData>(currentContent, currentPage, data);
            await FullOutModel<PageViewModel<TPage>, TPage>(catalogViewModel);
            return catalogViewModel;
        }
    }
}