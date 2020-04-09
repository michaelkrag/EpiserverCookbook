using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.MediaR
{
    public class CartAddResponce
    {
        public int QuantityAdded { get; set; } = 0;
        public string Code { get; set; } = "";
        public string ErrorMessages { get; set; } = "";
        public bool VariantAdded { get; set; }
    }
}