using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiserverCookBook.Models.ViewModels
{
    public interface ISeoMetaData
    {
        string MetaTitle { get; }
        string MetaDescription { get; }
        bool MetaAllowIndexing { get; }
    }
}