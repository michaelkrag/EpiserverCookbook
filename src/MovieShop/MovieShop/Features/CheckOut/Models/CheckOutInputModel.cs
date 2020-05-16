using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Features.CheckOut.Models
{
    public class CheckOutInputModel
    {
        public string firstName { get; set; }
        public string familyName { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string contry { get; set; }
    }
}