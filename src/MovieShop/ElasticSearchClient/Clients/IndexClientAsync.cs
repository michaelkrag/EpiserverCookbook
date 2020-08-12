using ElasticSearchClient.RestFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchClient.Clients
{
    public class IndexClientAsync
    {
        private readonly HttpClient _httpClient;
        public readonly string _index;

        public IndexClientAsync(string index, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _index = index;
        }

        public async Task<bool> DeleteDocument(string documentId)
        {
            var responce = await _httpClient.DeleteAsync(index);
        }

        //delete
        //update
        //delete
        //search
    }
}