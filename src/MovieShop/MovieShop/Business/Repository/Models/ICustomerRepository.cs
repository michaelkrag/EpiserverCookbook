namespace MovieShop.Business.Repository.Models
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(string email);

        Customer Insert(Customer customer);
    }
}