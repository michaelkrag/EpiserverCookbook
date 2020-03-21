using MovieShop.Foundation.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.Search
{
    public class SearchResultData
    {
        public string Query { get; set; }
        public IEnumerable<ISearch> SearcheResults { get; set; }
    }
}