using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MovieShop.Infrastructure.Helpers
{
    public static class HtmlMemberHelper
    {
        public static string GetMember<T>(this HtmlHelper helper, Expression<Func<T, object>> func)
        {
            if (func.Body is MemberExpression memberExpression)
            {
                return $"{typeof(T).Name}.{memberExpression.Member.Name}";
            }
            return string.Empty;
        }
    }
}