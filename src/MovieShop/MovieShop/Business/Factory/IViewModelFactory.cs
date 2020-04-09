using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using MovieShop.Models.ViewModels;

namespace MovieShop.Business.Factory
{
    public interface IViewModelFactory
    {
        PageViewModel<TPage> Create<TPage>(TPage currentPage) where TPage : PageData;

        PageViewModel<TPage, TData> Create<TPage, TData>(TPage currentPage, TData CurrentData) where TPage : PageData;

        CatalogViewModel<TCatalogContent, TPageData> CreateCatalog<TCatalogContent, TPageData>(TCatalogContent currentContent, TPageData currentPage) where TCatalogContent : EntryContentBase where TPageData : PageData;

        CatalogViewModel<TCatalogContent, TPage, TData> CreateCatalog<TCatalogContent, TPage, TData>(TCatalogContent currentContent, TPage currentPage, TData data) where TCatalogContent : EntryContentBase where TPage : PageData;
    }
}