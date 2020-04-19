using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Core;
using EPiServer.Core.Internal;
using MediatR;
using MovieShop.Business.Extensions;
using MovieShop.Business.Services.Prices;
using MovieShop.Domain.Commerce.Variants;
using MovieShop.Domain.MediaR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieShop.Services.Product
{
    public class ProductHandle : IRequestHandler<VariantsRequest, VariantsResponce>
    {
        private readonly IRelationRepository _relationRepository;
        private readonly IContentLoader _contentLoader;
        public readonly ICustomerPriceService _customerPriceService;

        public ProductHandle(IRelationRepository relationRepository, IContentLoader contentLoader, ICustomerPriceService customerPriceService)
        {
            _relationRepository = relationRepository;
            _contentLoader = contentLoader;
            _customerPriceService = customerPriceService;
        }

        public Task<VariantsResponce> Handle(VariantsRequest request, CancellationToken cancellationToken)
        {
            var variationReferences = _relationRepository.GetChildren<ProductVariation>(request.ProductReference);
            var variations = _contentLoader.GetItems(variationReferences.Select(x => x.Child), new LoaderOptions { LanguageLoaderOption.FallbackWithMaster() }).OfType<MovieVariant>();

            var prices = _customerPriceService.GetPrices(variations.Select(x => x.Code)).ToDictionary(x => x.CatalogKey.CatalogEntryCode, y => y);
            var discount = _customerPriceService.GetDiscountPrices(variations.Select(x => x.ContentLink)).ToDictionary(x => x.EntryLink, y => y.DiscountPrices);
            var result = variations.Select(x => new Variant()
            {
                Code = x.Code,
                Name = x.Name,
                Primary = x.IsPrimary,
                DisplayName = x.MediaTypes,
                NormalPrice = prices.GetPrice(x.Code),
                Discounts = discount.GetDiscounts(x.ContentLink)
            }).ToList();

            var responce = new VariantsResponce() { Variants = result };

            if (string.IsNullOrEmpty(request.CurrentVariantCode))
            {
                responce.ActiveVariant = result.Where(x => x.Primary).FirstOrDefault();
            }
            else
            {
                responce.ActiveVariant = result.Where(x => x.Code == request.CurrentVariantCode).FirstOrDefault();
            }
            return Task.FromResult(responce);
        }
    }
}