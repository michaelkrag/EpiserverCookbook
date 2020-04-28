using MovieShop.Domain.MediaR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.CheckOut.Models
{
    public class CheckoutModel
    {
        public string Step { get; set; } = "step1";
        public GetOrCreateCustomerResponce Customer { get; set; }

        public bool IsStep1 => Step == "step1";
    }
}