﻿using EPiServer.Core;
using EPiServer.DataAbstraction;
using MovieShop.Infrastructure.Helpers;

namespace MovieShop.Models.Pages
{
    public abstract class BasePage : PageData
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            SetDefaultHelper.MapDefaultValues(this);
        }
    }
}