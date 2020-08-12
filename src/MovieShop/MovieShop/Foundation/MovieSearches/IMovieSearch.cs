using MovieShop.Foundation.Search;
using System.Collections.Generic;

namespace MovieShop.Foundation.MovieSearches
{
    public interface IMovieSearch
    {
        IEnumerable<MovieDocument> SearchByGenre(string genre);
    }
}