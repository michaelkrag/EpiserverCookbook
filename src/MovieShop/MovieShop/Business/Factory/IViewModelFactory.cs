using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using MovieShop.Models.ViewModels;
using System.Threading.Tasks;

namespace MovieShop.Business.Factory
{
    public interface IViewModelFactory
    {
        Task<PageViewModel<TPage>> Create<TPage>(TPage currentPage) where TPage : PageData;

        Task<PageViewModel<TPage, TData>> Create<TPage, TData>(TPage currentPage, TData CurrentData) where TPage : PageData;

        Task<CatalogViewModel<TCatalogContent, TPageData>> CreateCatalog<TCatalogContent, TPageData>(TCatalogContent currentContent, TPageData currentPage) where TCatalogContent : CatalogContentBase where TPageData : PageData;

        Task<CatalogViewModel<TCatalogContent, TPage, TData>> CreateCatalog<TCatalogContent, TPage, TData>(TCatalogContent currentContent, TPage currentPage, TData data) where TCatalogContent : CatalogContentBase where TPage : PageData;
    }
}