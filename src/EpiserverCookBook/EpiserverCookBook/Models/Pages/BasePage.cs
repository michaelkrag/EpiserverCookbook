using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCookBook.Models.Pages
{
    public abstract class BasePage : PageData
    {
        public virtual bool AllowIndexing { get; set; }
        public virtual string MetaDescription { get; set; }

        public virtual string MetaTitle { get; set; }
    }
}