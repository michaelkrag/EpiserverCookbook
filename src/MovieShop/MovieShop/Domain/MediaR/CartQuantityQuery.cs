using MediatR;

namespace MovieShop.Domain.MediaR
{
    public class CartQuantityQuery : IRequest<int>
    {
        public static CartQuantityQuery Create()
        {
            return new CartQuantityQuery();
        }
    }
}