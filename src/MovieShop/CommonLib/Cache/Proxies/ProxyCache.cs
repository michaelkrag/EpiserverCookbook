using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Cache.Proxies
{
    public class ProxyCache<T> : RealProxy
    {
        private readonly T _instance;
        private readonly TimeSpan _timeOut;
        private readonly ICache _cache;
        private readonly IProxyKeyHandler _proxyKeyHandler;

        public ProxyCache(T instance, ICache cache, TimeSpan timeOut, IProxyKeyHandler proxyKeyHandler) : base(typeof(T))
        {
            _instance = instance;
            _timeOut = timeOut;
            _cache = cache;
            _proxyKeyHandler = proxyKeyHandler;
        }

        /// <summary>
        /// Create a proxy cache for any object
        /// </summary>
        /// <param name="instance">the object to cache</param>
        /// <param name="timeOut">how long the object will leve in</param>
        /// <returns>A cache verion of the instance</returns>
        public static T Create(T instance, ICache cache, TimeSpan timeOut, IProxyKeyHandler proxyKeyHandler = null)
        {
            return (T)new ProxyCache<T>(instance, cache, timeOut, proxyKeyHandler).GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = (IMethodCallMessage)msg;
            var method = (MethodInfo)methodCall.MethodBase;
            try
            {
                var key = CreateKey($"{method.DeclaringType.Name}.{method.Name}", methodCall.InArgs);
                if (_proxyKeyHandler != null)
                {
                    _proxyKeyHandler?.AddKey(key);
                }
                object value = _cache.GetOrCreate(key, () => method.Invoke(_instance, methodCall.InArgs), _timeOut, CacheDurationType.Absolute);
                return new ReturnMessage(value, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                if (e is TargetInvocationException && e.InnerException != null)
                {
                    return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
                }
                return new ReturnMessage(e, msg as IMethodCallMessage);
            }
        }

        public string CreateKey(string name, object[] args)
        {
            if (args == null || args.Count() == 0)
            {
                return name;
            }
            unchecked// Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + name.GetHashCode();
                foreach (var obj in args)
                {
                    hash = hash * 23 + (obj?.GetHashCode() ?? 0);
                }
                return hash.ToString();
            }
        }
    }
}