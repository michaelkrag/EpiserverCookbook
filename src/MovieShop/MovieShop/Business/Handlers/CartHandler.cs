using EPiServer;
using EPiServer.Commerce.Order;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Catalog;
using MediatR;
using MovieShop.Domain.Commerce.Variants;
using MovieShop.Domain.MediaR;
using MovieShop.Foundation.Extensions;
using MovieShop.Infrastructure.Factorys;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieShop.Business.Handlers
{
    public class CartHandler : IRequestHandler<CartAddRequest, CartAddResponce>
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
            var cart = _cartFactory.Create();
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

            _cartFactory.Save(cart);
            return await Task.FromResult(new CartAddResponce() { Code = request.Code, VariantAdded = true });
        }
    }
}