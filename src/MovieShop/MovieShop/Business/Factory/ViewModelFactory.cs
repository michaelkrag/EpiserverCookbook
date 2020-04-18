using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using Mediachase.Commerce;
using Mediachase.Commerce.Markets;
using MediatR;
using MovieShop.Domain.Component;
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
        private readonly IMarketService _marketService;
        private readonly ICurrentMarket _currentMarket;

        public ViewModelFactory(IMediator mediator, IMenuSettings menuSettings, IMarketService marketService, ICurrentMarket currentMarket)
        {
            _mediator = mediator;
            _menuSettings = menuSettings;
            _marketService = marketService;
            _currentMarket = currentMarket;
        }

        private async Task FullOutModel<TModel, TPage>(TModel viewModel) where TModel : PageViewModel<TPage> where TPage : PageData
        {
            viewModel.CartApiUrl = "/cart";
            viewModel.CartUrl = "/en/shooping-cart/";

            var currentMarket = _currentMarket.GetCurrentMarket();

            var markets = _marketService.GetAllMarkets();
            var maketsSelector = new List<SelectEntry>();

            foreach (var market in markets)
            {
                foreach (var currencie in market.Currencies)
                {
                    var code = MarketCurrency.Create(market.MarketId.Value, currencie.CurrencyCode);
                    maketsSelector.Add(new SelectEntry() { DisplayName = $"{market.MarketName} - {currencie.CurrencyCode}", Selected = market.MarketId.Value == currentMarket.MarketId.Value, Key = code.ToString() });
                }
            }

            viewModel.Markets = maketsSelector;

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