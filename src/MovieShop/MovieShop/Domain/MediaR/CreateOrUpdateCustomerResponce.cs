using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.MediaR
{
    public class CreateOrUpdateCustomerResponce
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string familyName { get; set; }
        public string CustomerId { get; set; }
    }
}