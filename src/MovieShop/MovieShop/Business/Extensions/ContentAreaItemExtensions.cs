using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace MovieShop.Business.Extensions
{
    public static class ContentAreaItemExtensions
    {
        private static Injected<IContentLoader> injected;
        private static IContentLoader _contentLoader => injected.Service;

        public static TType Get<TType>(this ContentAreaItem contentAreaItem) where TType : IContentData
        {
            if (contentAreaItem == null)
            {
                return default(TType);
            }
            var content = _contentLoader.Get<IContent>(contentAreaItem.ContentLink);
            if (content is TType rtnContent)
            {
                return rtnContent;
            }
            return default(TType);
        }
    }
}