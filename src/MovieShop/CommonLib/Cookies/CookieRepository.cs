using CommonLib.Cookies.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonLib.Cookies
{
    public class CookieRepository : ICookieRepository
    {
        private readonly bool _encodeCookie;
        private readonly ICookieHelper _cookieHelper;

        public CookieRepository(bool encodeCookie, ICookieHelper cookieHelper)
        {
            _encodeCookie = encodeCookie;
            _cookieHelper = cookieHelper;
        }

        public bool Set<T>(string name, TimeSpan date, T cookieObject)
        {
            var jsonStr = JsonConvert.SerializeObject(cookieObject);
            return _encodeCookie ?
                _cookieHelper.SetProtect(name, jsonStr, date) :
                _cookieHelper.Set(name, jsonStr, date);
        }

        public T Get<T>(string name)
        {
            try
            {
                var cookieString = _encodeCookie ?
                                        _cookieHelper.GetProtect(name) :
                                        _cookieHelper.Get(name);

                if (string.IsNullOrEmpty(cookieString))
                {
                    return default(T);
                }
                var obj = JsonConvert.DeserializeObject<T>(cookieString);
                return obj;
            }
            catch
            {
                return default(T);
            }
        }

        public T Get<T>(string name, HttpCookieCollection httpCookieCollection)
        {
            try
            {
                var cookieData = _cookieHelper.Get(name, httpCookieCollection);
                var cookieString = _encodeCookie ? _cookieHelper.Unprotect(cookieData, name) : cookieData;

                if (string.IsNullOrEmpty(cookieString))
                {
                    return default(T);
                }
                var obj = JsonConvert.DeserializeObject<T>(cookieString);
                return obj;
            }
            catch
            {
                return default(T);
            }
        }
    }
}