﻿using NLPLib.Vocabularys.Models;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace NLPLib.Vocabularys
{
    public class Vocabulary : IVocabulary
    {
        private ConcurrentDictionary<string, VocabularyEntry> _words = new ConcurrentDictionary<string, VocabularyEntry>();
        private int _index = 0;

        public int NumberOfWords => _index;

        private int GetNextIndex()
        {
            return Interlocked.Increment(ref _index);
        }

        public int GetOrAddIndex(string word)
        {
            var wordObj = _words.GetOrAdd(word, x => new VocabularyEntry(GetNextIndex()));
            wordObj.Occurred();
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

        public IEnumerator<WordInfo> GetEnumerator()
        {
            foreach (var word in _words)
            {
                yield return new WordInfo() { Word = word.Key, Occurs = word.Value.Occurs };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<VocabularyItem> Export()
        {
            foreach (var term in _words)
            {
                yield return new VocabularyItem() { Term = term.Key, Occurs = term.Value.Occurs, TermId = term.Value.TermId };
            }
        }

        public void Import(IEnumerable<VocabularyItem> terms)
        {
            _words = new ConcurrentDictionary<string, VocabularyEntry>();
            foreach (var term in terms)
            {
                _words.TryAdd(term.Term, new VocabularyEntry(term.TermId, term.Occurs));
            }
        }
    }
}