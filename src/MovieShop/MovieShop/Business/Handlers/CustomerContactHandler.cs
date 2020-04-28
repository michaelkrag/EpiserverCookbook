using Mediachase.Commerce.Customers;
using MediatR;
using MovieShop.Business.Repository.Models;
using MovieShop.Domain.MediaR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MovieShop.Business.Handlers
{
    public class CustomerContactHandler : IRequestHandler<GetOrCreateCustomerRequest, GetOrCreateCustomerResponce>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerContactHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // CustomerContext.Current
        // public AddCustomer()
        //{
        //    CustomerContact contact = _customerContext.GetContactByUsername(user.UserName);
        //    if (contact == null)
        //    {
        //        contact = CustomerContact.CreateInstance();
        //        contact.PrimaryKeyId = new PrimaryKeyId(new Guid(user.Id));
        //        contact.UserId = "String:" + user.Email; // The UserId needs to be set in the format "String:{email}". Else a duplicate CustomerContact will be created later on.
        //    }

        //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //    // Send an email with this link
        //    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //    if (!String.IsNullOrEmpty(user.FirstName) || !String.IsNullOrEmpty(user.LastName))
        //    {
        //        contact.FullName = $"{user.FirstName} {user.LastName}";
        //    }

        //    contact.FirstName = user.FirstName;
        //    contact.LastName = user.LastName;
        //    contact.Email = user.Email;
        //    contact.RegistrationSource = user.RegistrationSource;

        //    if (user.Addresses != null)
        //    {
        //        foreach (var address in user.Addresses)
        //        {
        //            contact.AddContactAddress(address);
        //        }
        //    }

        //    // The contact, or more likely its related addresses, must be saved to the database before we can set the preferred
        //    // shipping and billing addresses. Using an address id before its saved will throw an exception because its value
        //    // will still be null.
        //    contact.SaveChanges();
        //}
        public Task<GetOrCreateCustomerResponce> Handle(GetOrCreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var user = _customerRepository.GetCustomer(request.Email);
            if (user == null)
            {
                user = _customerRepository.Insert(
                    new Customer()
                    {
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.familyName
                    }
                    );
            }
            return Task.FromResult(new GetOrCreateCustomerResponce() { CustomerId = user.UserId, Email = user.Email, familyName = user.LastName, FirstName = user.FirstName });
        }
    }
}