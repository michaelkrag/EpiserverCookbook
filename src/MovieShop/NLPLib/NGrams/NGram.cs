using NLPLib.NGrams.Models;
using NLPLib.Search.Attributes;
using NLPLib.Tokenizers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.NGrams
{
    public class NGram : INGram
    {
        private ConcurrentDictionary<string, ContextWords> _nGams = new ConcurrentDictionary<string, ContextWords>();
        private readonly int _window;
        private readonly ISentencezer _sentencezer;

        public NGram(int window, ISentencezer sentencezer)
        {
            _window = window;
            _sentencezer = sentencezer;
        }

        public void Insert(string[] sentence)
        {
            var wordIndex = 0;

            while (wordIndex < sentence.Length)
            {
                var startIndex = wordIndex - _window / 2;
                startIndex = startIndex < 0 ? 0 : startIndex;
                var endIndex = wordIndex + _window / 2 + 1;
                endIndex = endIndex < sentence.Length ? endIndex : sentence.Length;
                var contextWords = _nGams.GetOrAdd(sentence[wordIndex], new ContextWords());
                for (var contextIndex = startIndex; contextIndex < endIndex; contextIndex++)
                {
                    if (contextIndex != wordIndex)
                    {
                        var inc = Math.Abs(1 / (double)(wordIndex - contextIndex));
                        contextWords.AddWord(sentence[contextIndex], inc);
                    }
                }
                wordIndex++;
            }
        }

        public void Insert<TObj>(TObj obj)
        {
            foreach (var member in typeof(TObj).GetProperties())
            {
                var attribute = member.GetCustomAttributes(typeof(IndexingAttribute), true).FirstOrDefault();
                if (attribute != null && member.PropertyType == typeof(string))
                {
                    var text = member.GetValue(obj, null) as string;
                    if (!string.IsNullOrEmpty(text))
                    {
                        foreach (var sen in _sentencezer.GetSentenc(text))
                        {
                            Insert(sen);
                        }
                    }
                }
            }
        }

        public IEnumerable<string> GetSuggestions(string word)
        {
            if (_nGams.TryGetValue(word, out var value))
            {
                return value.GetWords();
            }
            return Enumerable.Empty<string>();
        }

        public ConcurrentDictionary<string, ContextWords> Export()
        {
            return _nGams;
        }

        public void Impot(ConcurrentDictionary<string, ContextWords> ngram)
        {
            _nGams = ngram;
        }
    }
}