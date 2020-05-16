namespace MovieShop.Business.Repository.Models
{
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public string PostCode { get; set; }
        public string State { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}