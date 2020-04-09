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
            return new PageViewModel<TPage>(currentPage, currentPage.Name).SetLayout(currentPage);
        }

        public PageViewModel<TPage, TData> Create<TPage, TData>(TPage currentPage, TData CurrentData) where TPage : PageData
        {
            return new PageViewModel<TPage, TData>(currentPage, CurrentData).SetLayout(currentPage);
        }

        public CatalogViewModel<TCatalogContent, TPage> CreateCatalog<TCatalogContent, TPage>(TCatalogContent currentContent, TPage currentPage) where TCatalogContent : EntryContentBase where TPage : PageData
        {
            var catalogViewModel = new CatalogViewModel<TCatalogContent, TPage>(currentContent, currentPage);
            return catalogViewModel;
        }

        public CatalogViewModel<TCatalogContent, TPage, TData> CreateCatalog<TCatalogContent, TPage, TData>(TCatalogContent currentContent, TPage currentPage, TData data) where TCatalogContent : EntryContentBase where TPage : PageData
        {
            var catalogViewModel = new CatalogViewModel<TCatalogContent, TPage, TData>(currentContent, currentPage, data);
            return catalogViewModel;
        }
    }
}