using Mediachase.Commerce.Customers;
using MovieShop.Business.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Repository.Extensions
{
    public static class AddressExtensions
    {
        public static Address ToAddress(this CustomerAddress contactAddress)
        {
            return new Address
            {
                AddressLine1 = contactAddress.Line1,
                AddressLine2 = contactAddress.Line2,
                City = contactAddress.City,
                CountryCode = contactAddress.CountryCode,
                CountryName = contactAddress.CountryName,
                Email = contactAddress.Email,
                Phone1 = contactAddress.DaytimePhoneNumber,
                Phone2 = contactAddress.EveningPhoneNumber,
                PostCode = contactAddress.PostalCode,
                State = contactAddress.State,
                LastName = contactAddress.LastName,
                FirstName = contactAddress.FirstName
            };
        }

        public static CustomerAddress ToCustomerAddress(this Address address)
        {
            var customerAddress = CustomerAddress.CreateInstance();
            customerAddress.Line1 = address.AddressLine1;
            customerAddress.Line2 = address.AddressLine2;
            customerAddress.City = address.City;
            customerAddress.CountryCode = address.CountryCode;
            customerAddress.CountryName = address.CountryName;
            customerAddress.Email = address.Email;
            customerAddress.DaytimePhoneNumber = address.Phone1;
            customerAddress.EveningPhoneNumber = address.Phone2;
            customerAddress.PostalCode = address.PostCode;
            customerAddress.State = address.State;
            customerAddress.FirstName = address.FirstName;
            customerAddress.LastName = address.LastName;
            customerAddress.Name = $"{address.FirstName} {address.LastName}";
            return customerAddress;
        }
    }
}