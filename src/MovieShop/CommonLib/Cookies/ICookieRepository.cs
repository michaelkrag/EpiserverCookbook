using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonLib.Cookies
{
    public interface ICookieRepository
    {
        bool Set<T>(string name, TimeSpan date, T cookieObject);

        T Get<T>(string name);

        T Get<T>(string name, HttpCookieCollection httpCookieCollection);
    }
}