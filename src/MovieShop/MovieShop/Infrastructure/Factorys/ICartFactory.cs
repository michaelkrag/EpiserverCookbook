using EPiServer.Commerce.Order;

namespace MovieShop.Infrastructure.Factorys
{
    public interface ICartFactory
    {
        ICart Create(string name = "Default");

        void Save(ICart cart);
    }
}