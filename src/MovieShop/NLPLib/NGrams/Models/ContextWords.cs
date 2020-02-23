using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NLPLib.NGrams.Models
{
    public class ContextWords
    {
        public ConcurrentDictionary<string, double> _words = new ConcurrentDictionary<string, double>();

        public IEnumerable<SuggestionHit> GetWords()
        {
            return _words.OrderByDescending(x => x.Value).Select(x => new SuggestionHit() { term = x.Key, Score = x.Value });
        }

        public void AddWord(string word, double inc)
        {
            var contextCount = _words.GetOrAdd(word, 0);
            var newContextCount = contextCount + inc;
            _words.TryUpdate(word, newContextCount, contextCount);
        }
    }
}