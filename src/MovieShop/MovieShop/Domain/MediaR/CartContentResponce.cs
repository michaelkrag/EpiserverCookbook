using EPiServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.MediaR
{
    public class CartContentResponce
    {
        public IEnumerable<LineItem> LineItems { get; set; } = new List<LineItem>();
        public string Total { get; set; }
        public string OrderDiscount { get; set; }
        public bool HasOrderDiscount => !string.IsNullOrEmpty(OrderDiscount);
        public string ItemsDiscount { get; set; }
        public bool HasItemDiscount => !string.IsNullOrEmpty(ItemsDiscount);
        public string NoDiscount { get; set; }

        public bool HasDiscount()
        {
            if (HasOrderDiscount || HasItemDiscount)
            {
                return true;
            }
            return false;
        }
    }

    public class LineItem
    {
        public ContentReference ProductReference { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Media { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }
        public bool HasDiscount => !string.IsNullOrEmpty(DiscountPrice);
    }
}