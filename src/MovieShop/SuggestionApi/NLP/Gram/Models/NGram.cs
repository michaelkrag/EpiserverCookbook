using SuggestionApi.NLP.Gram.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SuggestionApi.NLP.Gram.Models
{
    public class NGram : IEnumerable<GramEntry>
    {
        private Dictionary<string, List<ContextWord>> _wordEntrys = new Dictionary<string, List<ContextWord>>();
        public readonly int Size;

        public NGram(int n)
        {
            Size = n;
        }

        public NGram(IEnumerable<GramEntry> wordEntrys, int n) : this(n)
        {
            foreach (var word in wordEntrys)
            {
                foreach (var contextWord in word.ContextWords)
                {
                    Add(word.Word, contextWord.Word, contextWord.Count);
                }
            }
        }

        public IEnumerator<GramEntry> GetEnumerator()
        {
            foreach (var wordEntry in _wordEntrys)
            {
                var entry = new GramEntry()
                {
                    Word = wordEntry.Key,
                    ContextWords = wordEntry.Value
                };
                yield return entry;
            }
        }

        public void Add(string[] words)
        {
            var wordList = new List<string>() { "<s>" };
            wordList.AddRange(words);
            wordList.Add(@"<\s>");

            for (var wordIndex = 0; wordIndex < wordList.Count - Size; wordIndex++)
            {
                var word = string.Join(" ", wordList.Skip(wordIndex).Take(Size));
                Add(word, wordList[wordIndex + Size]);
            }
        }

        public void Add(string key, string contextWord, int count = 1)
        {
            var contextWords = _wordEntrys.GetOrCreate(key, (x) => new List<ContextWord>());
            var context = contextWords.Where(x => x.Word == contextWord).FirstOrDefault();
            if (context == null)
            {
                contextWords.Add(new ContextWord() { Word = contextWord, Count = count });
            }
            else
            {
                context.Count += count;
            }
            _wordEntrys[key] = contextWords.OrderBy(x => x.Count).ToList();
        }

        public void Remove(string key, string contextWord, int count = 1)
        {
            if (_wordEntrys.TryGetValue(key, out var contextWords))
            {
                var context = contextWords.Where(x => x.Word == contextWord).FirstOrDefault();
                if (context != null)
                {
                    context.Count = -count;
                    if (context.Count < 1)
                    {
                        contextWords.Remove(context);
                    }
                }
                _wordEntrys[key] = contextWords.OrderBy(x => x.Count).ToList();
            }
        }

        public void Add(NGram otherGram)
        {
            foreach (var entry in otherGram)
            {
                foreach (var contextWord in entry.ContextWords)
                {
                    Add(entry.Word, contextWord.Word, contextWord.Count);
                }
            }
        }

        public IEnumerable<string> GetContextWords(string word)
        {
            if (_wordEntrys.TryGetValue(word, out var contextWords))
            {
                foreach (var contextWord in contextWords)
                {
                    yield return contextWord.Word;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}