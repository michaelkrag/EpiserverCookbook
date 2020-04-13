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
        private readonly ISearchEngine _searchEngine;
        private readonly IBiGram _biGram;
        private readonly ITriGram _triGram;

        public AutocompleateService(ITernarySearch ternarySearch, ISearchEngine searchEngine, IBiGram biGram, ITriGram triGram)
        {
            _ternarySearch = ternarySearch;
            _searchEngine = searchEngine;
            _biGram = biGram;
            _triGram = triGram;
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
                var result = _ternarySearch.Compleate(tokens.Last()).Take(7);
                var suggestions = new List<string>();
                if (tokens.Length < 2)
                {
                    foreach (var word in result)
                    {
                        var nextWord = _biGram.GetSuggestions(word).OrderByDescending(x => x.Score).Select(x => x.term);
                        if (nextWord.Count() > 0)
                        {
                            var biWord = $"{word} {nextWord.First()}";
                            nextWord = _triGram.GetSuggestions(biWord).OrderByDescending(x => x.Score).Select(x => x.term);
                            if (nextWord.Count() > 0)
                            {
                                suggestions.Add($"{biWord} {nextWord.First()}");
                            }
                            else
                            {
                                suggestions.Add($"{biWord}");
                            }
                        }
                        else
                        {
                            suggestions.Add($"{word}");
                        }
                    }
                }
                else
                {
                    foreach (var word in result)
                    {
                        suggestions.Add($"{string.Join(" ", tokens.Take(tokens.Length - 1))} {word}");
                    }
                }
                //        var suggesions = _irtRetSearch.Search<ISearch>(query, 5).ToList();
                return suggestions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}