using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using System.Web.Mvc;

namespace MovieShop.Business.Services.ImageStore
{
    public static class UrlResolverExtension
    {
        public static ContentReference GetContent(this IUrlResolver urlResolver, string url)
        {
            IContent contentData = urlResolver.Route(new UrlBuilder(url));
            if (contentData == null)
            {
                return ContentReference.EmptyReference;
            }
            return contentData.ContentLink;
        }

        public static string ContentUrl(this UrlHelper urlHelper, ContentReference contentLink)
        {
            var url = urlHelper.ContentUrl(contentLink);
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }
            return url;
        }
    }
}