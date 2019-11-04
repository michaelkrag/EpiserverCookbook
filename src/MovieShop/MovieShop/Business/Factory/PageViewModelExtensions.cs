using EPiServer.Core;
using MovieShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Factory
{
    public static class PageViewModelExtensions
    {
        public static T SetLayout<T, TPage>(this T viewModel, TPage currentPage) where T : PageViewModel<TPage> where TPage : PageData
        {
            viewModel.Title = currentPage.Name;
            return viewModel;
        }
    }
}