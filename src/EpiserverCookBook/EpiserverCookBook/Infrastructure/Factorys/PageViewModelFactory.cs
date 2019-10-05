using EPiServer.Core;
using EpiserverCookBook.Infrastructure.Factorys.Extensions;
using EpiserverCookBook.Models.Pages;
using EpiserverCookBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCookBook.Infrastructure.Factorys
{
    public class PageViewModelFactory : IPageViewModelFactory
    {
        public PageViewModelFactory()
        {
        }

        public PageViewModel<TPage> Create<TPage>(TPage currentPage) where TPage : BasePage
        {
            var viewModel = new PageViewModel<TPage>(currentPage);

            return viewModel.SetData<PageViewModel<TPage>, TPage>();
        }

        public PageViewModel<TPage, TData> Create<TPage, TData>(TPage currentPage, TData data) where TPage : BasePage
        {
            if (currentPage == null) throw new ArgumentNullException(nameof(currentPage));
            if (data == null) throw new ArgumentNullException(nameof(data));
            return new PageViewModel<TPage, TData>(currentPage, data).SetData<PageViewModel<TPage, TData>, TPage>();
        }
    }
}