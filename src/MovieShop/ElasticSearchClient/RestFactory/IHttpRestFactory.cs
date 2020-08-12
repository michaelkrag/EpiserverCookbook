using System;
using System.Net.Http;

namespace ElasticSearchClient.RestFactory
{
    public interface IHttpRestFactory
    {
        HttpClient CreateOrGet(string name, Func<HttpClient> func);
    }
}