using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Domain.MediaR
{
    public class CartAddRequest : IRequest<CartAddResponce>
    {
        public int Quantity { get; private set; }
        public string Code { get; private set; }

        public CartAddRequest(string code, int quantity)
        {
            Code = code;
            Quantity = quantity;
        }

        public static CartAddRequest Create(string code, int quantity)
        {
            return new CartAddRequest(code, quantity);
        }
    }
}