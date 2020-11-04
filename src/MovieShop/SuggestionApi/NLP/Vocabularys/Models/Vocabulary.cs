using SuggestionApi.InformationRetrieval;
using SuggestionApi.Models;
using SuggestionApi.NLP.Vocabularys.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SuggestionApi.NLP.Vocabularys
{
    public class Vocabulary : ISearchVocabulary
    {
        private ConcurrentDictionary<string, VocabularyEntry> _words = new ConcurrentDictionary<string, VocabularyEntry>();
        private int _index = 0;
        public int NumberOfWords => _index;

        public Vocabulary()
        {
        }

        public Vocabulary(VocabularyFile vocabularyFile)
        {
            _index = vocabularyFile.NumberOfWords;
            foreach (var word in vocabularyFile.VocabularyEntries)
            {
                _words[word.Term] = word;
            }
        }

        private int GetNextIndex()
        {
            return Interlocked.Increment(ref _index);
        }

        public IEnumerable<VocabularyEntry> GetAll()
        {
            foreach (var word in _words)
            {
                yield return word.Value;
            }
        }

        public IEnumerable<Token> Add(string[] words)
        {
            var tokens = new List<Token>();
            foreach (var word in words)
            {
                var id = GetOrAddIndex(word);
                tokens.Add(Token.Create(word, id));
            }
            return tokens;
        }

        public int GetOrAddIndex(string word)
        {
            var wordObj = _words.GetOrAdd(word, x => VocabularyEntry.Create(GetNextIndex(), x));
            wordObj.IncementOccurred();
            return wordObj.TermId;
        }

        public int GetIndex(string word)
        {
            if (_words.TryGetValue(word, out var wordId))
            {
                return wordId.TermId;
            }
            return -1;
        }
    }
}