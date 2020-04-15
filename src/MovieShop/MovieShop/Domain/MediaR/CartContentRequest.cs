using MediatR;

namespace MovieShop.Domain.MediaR
{
    public class CartContentRequest : IRequest<CartContentResponce>
    {
        public static CartContentRequest Create()
        {
            return new CartContentRequest();
        }
    }
}