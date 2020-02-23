using MovieShop.Foundation.Search;
using NLPLib.NGrams;
using NLPLib.NGrams.Models;
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
        private readonly INGram _iNGram;

        public AutocompleateService(ITernarySearch ternarySearch, IIrtRetSearch irtRetSearch, INGram iNGram)
        {
            _ternarySearch = ternarySearch;
            _irtRetSearch = irtRetSearch;
            _iNGram = iNGram;
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
                foreach (var term in result)
                {
                    PredictQuery(tokens.Take(tokens.Length - 1), term);
                }

                var newQuery = string.Join(" ", tokens.Take(tokens.Length - 1).ToList());

                var suggestions = list.Select(x => $"{newQuery} {x}");

                //        var suggesions = _irtRetSearch.Search<ISearch>(query, 5).ToList();

                return suggestions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<SuggestionHit> PredictQuery(IEnumerable<string> queryTerms, string currentTerm)
        {
            var suggestion = new List<IEnumerable<SuggestionHit>>();
            var all = new List<string>(queryTerms);
            all.Add(currentTerm);
            foreach (var term in all)
            {
                suggestion.Add(_iNGram.GetSuggestions(term));
            }
            return suggestion.First();
        }
    }
}