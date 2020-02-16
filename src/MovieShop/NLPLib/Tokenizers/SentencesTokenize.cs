using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Tokenizers
{
    public class SentencesTokenize
    {
        private readonly Dictionary<string, string> _abbreviations;
        private readonly HashSet<string> _stopWords;

        public SentencesTokenize(Dictionary<string, string> abbreviations, HashSet<string> stopWords)
        {
            _abbreviations = abbreviations;
            _stopWords = stopWords;
        }

        public IEnumerable<string> GetSentences(string corpus)
        {
            if (!string.IsNullOrEmpty(corpus))
            {
                var sb = new StringBuilder();
                foreach (var word in corpus.Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.ToLower()))
                {
                    if (!_stopWords.Contains(word))
                    {
                        if (word.EndsWith(".") && !_abbreviations.ContainsKey(word))
                        {
                            var rest = word.Replace(".", "");
                            if (!string.IsNullOrEmpty(rest))
                            {
                                sb.Append($" {word.Replace(".", "")}");
                            }
                            var rtnStr = sb.ToString();
                            sb = new StringBuilder();
                            yield return rtnStr;
                        }
                        else if (!string.IsNullOrEmpty(word))
                        {
                            sb.Append($" {word.Replace(",", "")}");
                        }
                    }
                }
                if (sb.Length > 0)
                {
                    yield return sb.ToString();
                }
            }
        }
    }
}