using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieShop.Infrastructure.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlString SetIf(this HtmlHelper helper, bool setValue, string value)
        {
            var str = string.Empty;
            if (setValue)
            {
                str = value;
            }
            return new HtmlString(str);
        }

        public static IHtmlString SetReadOnly(this HtmlHelper helper, bool setReadonly)
        {
            return helper.SetIf(setReadonly, "disabled=\"disabled\" readonly");
        }
    }
}