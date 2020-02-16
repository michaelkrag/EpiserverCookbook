using System;
using System.Web;

namespace CommonLib.Cookies.Helpers
{
    public interface ICookieHelper
    {
        bool Set(string name, string data, TimeSpan date);

        string Get(string name);

        string Get(string name, HttpCookieCollection httpCookieCollection);

        bool SetProtect(string name, string data, TimeSpan date);

        string GetProtect(string name);

        string Protect(string text, string purpose);

        string Unprotect(string text, string purpose);
    }
}