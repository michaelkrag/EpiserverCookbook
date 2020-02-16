using System.Web;
using System.Web.Mvc;

namespace MovieShop.Foundation.Helpers
{
    public static class ImageHelper
    {
        public static IHtmlString MovieDbImage(this HtmlHelper htmlHelper, string url, int size)
        {
            if (string.IsNullOrEmpty(url))
            {
                return MvcHtmlString.Empty;
            }
            return new MvcHtmlString($"https://image.tmdb.org/t/p/w{size}/{url}");
        }
    }
}