using EPiServer.Core;
using MediatR;

namespace MovieShop.Domain.MediaR
{
    public class CategoryRequest : IRequest<CategoryResponce>
    {
        public ContentReference RootNode { get; }

        public CategoryRequest(ContentReference rootNode)
        {
            RootNode = rootNode;
        }

        public static CategoryRequest Create(ContentReference rootNode)
        {
            return new CategoryRequest(rootNode);
        }
    }
}