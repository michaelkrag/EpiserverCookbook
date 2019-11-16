﻿using System.Collections.Generic;
using System.Linq;

namespace NLPLib.Tokenizer
{
    public class WordTokenize
    {
        private readonly Dictionary<string, string> _abbreviations;
        private readonly HashSet<string> _stopWords;

        public WordTokenize(Dictionary<string, string> abbreviations, HashSet<string> stopWords)
        {
            _abbreviations = abbreviations;
            _stopWords = stopWords;
        }

        public IEnumerable<string> GetTokens(string sentencen)
        {
            var words = sentencen.Split(' ', '(', ')', '"', ':', ';', '!').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.ToLower());

            foreach (var word in words)
            {
                if (!_stopWords.Contains(word))
                {
                    yield return _abbreviations.TryGetValue(word, out var newWord) ? newWord : word;
                }
            }
        }
    }
}