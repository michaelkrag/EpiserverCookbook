using EPiServer.Core;
using MovieShop.Models.ViewModels;

namespace MovieShop.Business.Factory
{
    public interface IViewModelFactory
    {
        PageViewModel<TPage> Create<TPage>(TPage currentPage) where TPage : PageData;

        PageViewModel<TPage, TData> Create<TPage, TData>(TPage currentPage, TData CurrentData) where TPage : PageData;
    }
}