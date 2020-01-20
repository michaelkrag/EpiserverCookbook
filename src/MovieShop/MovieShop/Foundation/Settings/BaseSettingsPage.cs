using EPiServer.Core;
using EPiServer.DataAnnotations;
using MovieShop.Models.Pages;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieShop.Foundation.Settings
{
    public abstract class BaseSettingsPage : BasePage, ISettingsPage
    {
        [Display(Name = "Settings", Description = "")]
        [AllowedTypes(new Type[] { typeof(ISettingsBlock) })]
        public virtual ContentArea SettingsArea { get; set; }
    }
}