using NLPLib.Tools.TernaryTree;
using NLPLib.Tools.Wordbook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Tools.Spelling
{
    public class SpellCorrector<TObj>
    {
        private readonly char[] _alphabet;
        private readonly ISearch<TObj> _vocabulary;

        public SpellCorrector(ISearch<TObj> vocabulary, char[] alphabet)
        {
            _alphabet = alphabet;
            _vocabulary = vocabulary;
        }

        private IEnumerable<string> Edits1(string word)
        {
            var splits = Enumerable.Range(0, word.Length).Select(x => new { a = word.Substring(0, x), b = word.Substring(x) });
            var deletes = splits.Select(x => x.a + x.b.Substring(1));
            var transposes = splits.Where(x => x.b.Length > 1).Select(x => x.a + x.b[1] + x.b[0] + x.b.Substring(2));

            var replaces = splits.SelectMany(w => _alphabet.Select(c => w.a + c + w.b.Substring(1)));

            var inserts = Enumerable.Range(0, word.Length + 1).Select(x => new { a = word.Substring(0, x), b = word.Substring(x) }).SelectMany(w => _alphabet.Select(c => w.a + c + w.b));

            return deletes.Union(deletes).Union(transposes).Union(replaces).Union(inserts);
        }

        public IEnumerable<TObj> Candidates(string word)
        {
            var candidates = _vocabulary.Search(word);
            if (candidates != null && candidates.Any())
            {
                return candidates;
            }
            candidates = _vocabulary.Search(Edits1(word));
            if (candidates != null && candidates.Any())
            {
                return candidates;
            }
            return Enumerable.Empty<TObj>();
        }
    }
}