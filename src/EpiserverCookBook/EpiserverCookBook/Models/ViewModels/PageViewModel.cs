using System;

namespace EpiserverCookBook.Models.ViewModels
{
    public class PageViewModel<TPage> : BasePageViewModel
    {
        public TPage CurrentPage { get; }

        public PageViewModel(TPage currentPage)
        {
            CurrentPage = currentPage;
        }
    }

    public interface IPageViewModel<out TPage, out TData>
    {
        TPage CurrentPage { get; }
        TData DataModel { get; }
    }

    public class PageViewModel<TPage, TData> : PageViewModel<TPage>, IPageViewModel<TPage, TData>
    {
        public readonly string UniqPageViewId = "a" + Guid.NewGuid().ToString();
        public TData DataModel { get; }

        public PageViewModel(TPage currentPage, TData dataModel) : base(currentPage)
        {
            DataModel = dataModel;
        }
    }
}