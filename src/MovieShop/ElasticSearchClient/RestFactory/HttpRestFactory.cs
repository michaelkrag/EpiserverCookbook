using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchClient.RestFactory
{
    public class HttpRestFactory : IHttpRestFactory
    {
        private static ConcurrentDictionary<string, HttpClient> _clientDictionary = new ConcurrentDictionary<string, HttpClient>();

        public HttpClient CreateOrGet(string name, Func<HttpClient> func)
        {
            return _clientDictionary.GetOrAdd(name, x => func());
        }
    }
}