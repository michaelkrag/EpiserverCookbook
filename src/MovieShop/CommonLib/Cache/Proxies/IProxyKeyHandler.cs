using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Cache.Proxies
{
    public interface IProxyKeyHandler
    {
        void AddKey(string key);
    }
}