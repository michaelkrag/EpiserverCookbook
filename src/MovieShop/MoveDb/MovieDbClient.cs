using MoveDb.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoveDb
{
    public class MovieDbClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string ApiKey = "9502dc9a0340e45ce6368756a918751a";
        private const string MovieDbApi = "https://api.themoviedb.org/3";

        public MovieDbClient()
        {
        }

        public async Task<MovieDetails> GetMovieDetails(int movieId)
        {
            var url = $"{MovieDbApi}/movie/{movieId}?api_key={ApiKey}";
            return await GetEntryAsync<MovieDetails>(url);
        }

        public async Task<CastAndCrew> GetCastAndCrew(int movieId)
        {
            var url = $"{MovieDbApi}/movie/{movieId}/credits?api_key={ApiKey}";
            return await GetEntryAsync<CastAndCrew>(url);
        }

        public async Task<Credit> GetCredit(string creditId)
        {
            var url = $"{MovieDbApi}/credit/{creditId}?api_key={ApiKey}";
            return await GetEntryAsync<Credit>(url);
        }

        private async Task<TEntry> GetEntryAsync<TEntry>(string url)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntry>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        public async Task<byte[]> GetImage(string imageId, int size = 300)
        {
            var url = $"https://image.tmdb.org/t/p/w{size}/{imageId}";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsByteArrayAsync();
                return data;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }
    }
}