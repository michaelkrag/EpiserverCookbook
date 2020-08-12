using MovieShop.Foundation.Search;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Foundation.MovieSearches
{
    public class MovieSearch : IMovieSearch
    {
        private readonly ElasticClient _elasticClient;

        public MovieSearch(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public IEnumerable<MovieDocument> SearchByGenre(string genre)
        {
            var firstSearchResponse = _elasticClient.Search<MovieDocument>(s => s
                                                                    .Query(q => +q.Term(p => p.Genres, genre))
                                                                    .Sort(x => x.Descending(y => y.VoteAverage))
                                                                );
            return firstSearchResponse.Documents.ToList();
        }
    }
}