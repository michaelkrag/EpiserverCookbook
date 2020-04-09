using EPiServer.Core;
using MediatR;

namespace MovieShop.Domain.MediaR
{
    public class VariantsRequest : IRequest<VariantsResponce>
    {
        public ContentReference ProductReference { get; private set; }
        public string CurrentVariantCode { get; private set; }

        public VariantsRequest(ContentReference productReference, string variantCode)
        {
            ProductReference = productReference;
            CurrentVariantCode = variantCode;
        }

        public static VariantsRequest Create(ContentReference productReference, string variantCode)
        {
            return new VariantsRequest(productReference, variantCode);
        }
    }
}