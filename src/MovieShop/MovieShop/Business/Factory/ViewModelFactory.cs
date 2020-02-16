using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using MovieShop.Models.ViewModels;

namespace MovieShop.Business.Factory
{
    public class ViewModelFactory : IViewModelFactory
    {
        public ViewModelFactory()
        {
        }

        public PageViewModel<TPage> Create<TPage>(TPage currentPage) where TPage : PageData
        {
            return new PageViewModel<TPage>(currentPage).SetLayout(currentPage);
        }

        public PageViewModel<TPage, TData> Create<TPage, TData>(TPage currentPage, TData CurrentData) where TPage : PageData
        {
            return new PageViewModel<TPage, TData>(currentPage, CurrentData).SetLayout(currentPage);
        }

        public CatalogViewModel<TCatalogContent, TPageData> CreateCatalog<TCatalogContent, TPageData>(TCatalogContent currentContent, TPageData currentPage) where TCatalogContent : EntryContentBase where TPageData : PageData
        {
            CatalogViewModel<TCatalogContent, TPageData> catalogViewModel = new CatalogViewModel<TCatalogContent, TPageData>(currentContent, currentPage);
            return catalogViewModel;
        }
    }
}