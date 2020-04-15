using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.MediaR
{
    public class CartContentResponce
    {
        public IEnumerable<LineItem> LineItems { get; set; } = new List<LineItem>();
    }

    public class LineItem
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Media { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}