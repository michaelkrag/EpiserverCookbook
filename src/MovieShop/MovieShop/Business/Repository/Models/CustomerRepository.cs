using Mediachase.BusinessFoundation.Data;
using Mediachase.Commerce.Customers;
using System;

namespace MovieShop.Business.Repository.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerContext _customerContext = CustomerContext.Current;

        public Customer Insert(Customer customer)
        {
            var contact = CustomerContact.CreateInstance();
            contact.FirstName = customer.FirstName;
            contact.LastName = customer.LastName;
            contact.FullName = $"{customer.FirstName} {customer.LastName}";
            contact.Email = customer.Email;
            contact.PrimaryKeyId = new PrimaryKeyId(Guid.NewGuid());
            contact.UserId = customer.UserId;
            var result = contact.SaveChanges();

            return new Customer() { Email = result.Email, FirstName = result.FirstName, LastName = result.LastName };
        }

        public Customer GetCustomer(string email)
        {
            var userId = Customer.CreateUserId(email);
            CustomerContact contact = _customerContext.GetContactByUserId(userId);
            if (contact == null)
            {
                return null;
            }
            return new Customer()
            {
                Email = contact.Email,
                LastName = contact.LastName,
                FirstName = contact.FirstName
            };
        }
    }
}