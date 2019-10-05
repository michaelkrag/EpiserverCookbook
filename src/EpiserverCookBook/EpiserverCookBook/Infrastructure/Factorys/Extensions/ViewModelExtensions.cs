using EpiserverCookBook.Models.Pages;
using EpiserverCookBook.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCookBook.Infrastructure.Factorys.Extensions
{
    public static class ViewModelExtensions
    {
        public static TModel SetSeoMetaData<TModel, TPage>(this TModel model) where TPage : BasePage where TModel : PageViewModel<TPage>
        {
            model.MetaAllowIndexing = model.CurrentPage.AllowIndexing;
            model.MetaDescription = model.CurrentPage.MetaDescription;
            model.MetaTitle = model.CurrentPage.MetaTitle;
            return model;
        }

        public static TModel SetData<TModel, TPage>(this TModel model) where TPage : BasePage where TModel : PageViewModel<TPage>
        {
            return model.SetSeoMetaData<TModel, TPage>();
        }
    }
}