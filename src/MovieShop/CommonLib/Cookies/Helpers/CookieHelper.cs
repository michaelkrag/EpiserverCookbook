using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace CommonLib.Cookies.Helpers
{
    public class CookieHelper : ICookieHelper
    {
        public bool Set(string name, string data, TimeSpan date)
        {
            if (HttpContext.Current == null) return false;

            if (HttpContext.Current.Response.Cookies.AllKeys.Contains(name))
            {
                return UpdateCookie(name, data);
            }
            return CreateCookie(name, data, date);
        }

        private bool UpdateCookie(string name, string data)
        {
            if (HttpContext.Current == null) return false;

            if (HttpContext.Current.Response.Cookies.AllKeys.Contains(name))
            {
                var oldCookie = HttpContext.Current.Response.Cookies[name];
                oldCookie.Value = data;
                return true;
            }
            return false;
        }

        public bool CreateCookie(string name, string data, TimeSpan date)
        {
            if (HttpContext.Current == null) return false;
            if (!HttpContext.Current.Response.Cookies.AllKeys.Contains(name))
            {
                var userCookie = new HttpCookie(name, data);
                userCookie.Expires = DateTime.UtcNow.Add(date);
                HttpContext.Current.Response.SetCookie(userCookie);
                return true;
            }
            return false;
        }

        public string Get(string name)
        {
            if (HttpContext.Current == null) return string.Empty;

            if (HttpContext.Current.Response.Cookies.AllKeys.Contains(name))
            {
                var responceCookie = HttpContext.Current.Response.Cookies[name];
                return responceCookie.Value;
            }

            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(name))
            {
                var cookieString = HttpContext.Current.Request.Cookies[name];
                return cookieString.Value;
            }
            return string.Empty;
        }

        public string Get(string name, HttpCookieCollection httpCookieCollection)
        {
            if (httpCookieCollection.AllKeys.Contains(name))
            {
                var responceCookie = httpCookieCollection[name];
                return responceCookie.Value;
            }
            return string.Empty;
        }

        public bool SetProtect(string name, string data, TimeSpan date)
        {
            var encodeData = Protect(data, name);
            return Set(name, encodeData, date);
        }

        public string GetProtect(string name)
        {
            var cookieData = Get(name);
            return Unprotect(cookieData, name);
        }

        public string Protect(string text, string purpose)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var stream = Encoding.UTF8.GetBytes(text);
            var encodedValue = MachineKey.Protect(stream, purpose);
            return HttpServerUtility.UrlTokenEncode(encodedValue);
        }

        public string Unprotect(string text, string purpose)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var stream = HttpServerUtility.UrlTokenDecode(text);
            var decodedValue = MachineKey.Unprotect(stream, purpose);
            return Encoding.UTF8.GetString(decodedValue);
        }
    }
}