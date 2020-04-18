using EPiServer;
using EPiServer.Commerce.Order;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Pricing;
using MediatR;
using MovieShop.Domain.Commerce.Variants;
using MovieShop.Domain.MediaR;
using MovieShop.Foundation.Extensions;
using MovieShop.Infrastructure.Factorys;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieShop.Business.Handlers
{
    public class CartHandler : IRequestHandler<CartAddRequest, CartAddResponce>, IRequestHandler<CartQuantityQuery, int>, IRequestHandler<CartContentRequest, CartContentResponce>
    {
        private readonly ICartFactory _cartFactory;
        private readonly IOrderGroupFactory _orderGroupFactory;
        private readonly IContentLoader _contentLoader;
        private readonly ReferenceConverter _referenceConverter;

        public CartHandler(ICartFactory cartFactory, IOrderGroupFactory orderGroupFactory, IContentLoader contentLoader, ReferenceConverter referenceConverter)
        {
            _cartFactory = cartFactory;
            _orderGroupFactory = orderGroupFactory;
            _contentLoader = contentLoader;
            _referenceConverter = referenceConverter;
        }

        public async Task<CartAddResponce> Handle(CartAddRequest request, CancellationToken cancellationToken)
        {
            var cart = _cartFactory.LoadOrCreateCart();
            var variantReference = _referenceConverter.GetContentLink(request.Code);
            if (variantReference == null)
            {
                return new CartAddResponce() { Code = request.Code, ErrorMessages = "Code dos not excist", VariantAdded = false };
            }
            var content = _contentLoader.Get<MovieVariant>(variantReference);
            if (content == null)
            {
                return new CartAddResponce() { Code = request.Code, ErrorMessages = "Can't load variant", VariantAdded = false };
            }

            cart.InsertOrUpdate(request.Code, request.Quantity, content.DisplayName);
            cart.ValidateCart();
            _cartFactory.Save(cart);
            return await Task.FromResult(new CartAddResponce() { Code = request.Code, VariantAdded = true, QuantityAdded = request.Quantity });
        }

        public async Task<int> Handle(CartQuantityQuery request, CancellationToken cancellationToken)
        {
            var cart = _cartFactory.LoadOrCreateCart();
            if (cart == null)
            {
                return await Task.FromResult(0);
            }
            var quantity = cart.GetAllLineItems().Sum(x => x.Quantity);
            return await Task.FromResult(Convert.ToInt32(quantity));
        }

        public async Task<CartContentResponce> Handle(CartContentRequest request, CancellationToken cancellationToken)
        {
            var cart = _cartFactory.LoadOrCreateCart();
            var lineItems = cart.GetAllLineItems().Select(x => new LineItem() { Code = x.Code, DisplayName = x.DisplayName, Quantity = Convert.ToInt32(x.Quantity) });

            var model = new CartContentResponce()
            {
                LineItems = lineItems
            };

            return await Task.FromResult(model);
        }
    }
}