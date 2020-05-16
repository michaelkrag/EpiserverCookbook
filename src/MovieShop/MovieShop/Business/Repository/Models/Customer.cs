using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Repository.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserId => CreateUserId(Email);

        public IEnumerable<Address> Addresses { get; set; } = Enumerable.Empty<Address>();

        public static string CreateUserId(string email)
        {
            return "String:" + email;
        }
    }
}