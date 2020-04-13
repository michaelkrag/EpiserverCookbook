using MovieShop.Foundation.Search;
using NLPLib.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.Category
{
    public class CategoryViewModel
    {
        public IEnumerable<SearchHit<ISearch>> SearchHits { get; set; }
    }
}