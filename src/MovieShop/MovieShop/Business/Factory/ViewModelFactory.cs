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
            PageViewModel<TPage> pageViewModel = new PageViewModel<TPage>(currentPage);
            return pageViewModel;
        }

        public PageViewModel<TPage, TData> Create<TPage, TData>(TPage currentPage, TData CurrentData) where TPage : PageData
        {
            PageViewModel<TPage, TData> pageViewModel = new PageViewModel<TPage, TData>(currentPage, CurrentData);
            return pageViewModel;
        }
    }
}