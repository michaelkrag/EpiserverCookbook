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
        public CreateOrUpdateCustomerResponce Customer { get; set; }
        public CartContentResponce Cart { get; set; }
        public bool IsStep1 => Step == "step1";
        public bool IsStep2 => Step == "step2";
        public bool IsStep3 => Step == "step3";
    }
}