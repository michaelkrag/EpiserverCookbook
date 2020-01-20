using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace MovieShop.Foundation.Extensions
{
    public static class ContentAreaItemExtension
    {
        private static Injected<IContentLoader> contentLoaderInjected;
        private static IContentLoader _contentLoader => contentLoaderInjected.Service;

        public static TType Get<TType>(this ContentAreaItem contentAreaItem) where TType : IContentData
        {
            if (contentAreaItem is null)
            {
                return default(TType);
            }
            var content = _contentLoader.Get<IContentData>(contentAreaItem.ContentLink);
            if (content is TType rtnContent)
            {
                return rtnContent;
            }
            return default(TType);
        }
    }
}