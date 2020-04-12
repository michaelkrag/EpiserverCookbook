using EPiServer;
using EPiServer.Commerce.Catalog.Linking;
using MediatR;
using MovieShop.Domain.Commerce.Nodes;
using MovieShop.Domain.MediaR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieShop.Business.Handlers
{
    public class CategoryHandler : IRequestHandler<CategoryRequest, CategoryResponce>
    {
        private readonly IContentLoader _contentLoader;

        public CategoryHandler(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public Task<CategoryResponce> Handle(CategoryRequest request, CancellationToken cancellationToken)
        {
            if (request.RootNode == null)
            {
                return Task.FromResult(CategoryResponce.Create(Enumerable.Empty<CategoryEntry>()));
            }
            var categories = _contentLoader.GetChildren<GenreNode>(request.RootNode);
            var rtnList = categories.Select(x => new CategoryEntry() { Link = x.ContentLink, Title = x.Name });
            return Task.FromResult(CategoryResponce.Create(rtnList));
        }
    }
}