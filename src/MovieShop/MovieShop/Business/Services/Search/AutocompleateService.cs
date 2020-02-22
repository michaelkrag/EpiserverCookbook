using MovieShop.Foundation.Search;
using NLPLib.Search;
using NLPLib.TernaryTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Business.Services.Search
{
    public class AutocompleateService : IAutocompleateService
    {
        private readonly ITernarySearch _ternarySearch;
        private readonly IIrtRetSearch _irtRetSearch;

        public AutocompleateService(ITernarySearch ternarySearch, IIrtRetSearch irtRetSearch)
        {
            _ternarySearch = ternarySearch;
            _irtRetSearch = irtRetSearch;
        }

        public IEnumerable<string> GetSuggestions(string query)
        {
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return Enumerable.Empty<string>();
                }
                var tokens = query.Split(' ');
                var result = _ternarySearch.Compleate(tokens.Last());
                var list = result.Select(x => x).Take(7);

                var newQuery = string.Join(" ", tokens.Take(tokens.Length - 1).ToList());

                var suggestions = list.Select(x => $"{newQuery} {x}");

                //      var suggesions = _irtRetSearch.Search<ISearch>(tryQuerys).OrderByDescending(x => x.Score).Take(8).Select(x => x.Document.Title).ToList();

                return suggestions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}