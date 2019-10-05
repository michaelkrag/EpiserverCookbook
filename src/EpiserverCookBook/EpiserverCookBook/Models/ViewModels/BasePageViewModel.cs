using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCookBook.Models.ViewModels
{
    public class BasePageViewModel : ISeoMetaData, IFooter, IHeader
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public bool MetaAllowIndexing { get; set; }
    }
}