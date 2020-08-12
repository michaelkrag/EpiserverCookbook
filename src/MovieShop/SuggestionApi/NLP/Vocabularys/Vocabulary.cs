using SuggestionApi.NLP.Vocabularys.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SuggestionApi.NLP.Vocabularys
{
    public class Vocabulary : IVocabulary
    {
        private ConcurrentDictionary<string, VocabularyEntry> _words = new ConcurrentDictionary<string, VocabularyEntry>();
        private readonly IVocabularyRepository _vocabularyRepository;
        private int _index = 0;

        private int GetNextIndex()
        {
            return Interlocked.Increment(ref _index);
        }

        public Vocabulary(IVocabularyRepository vocabularyRepository)
        {
            _vocabularyRepository = vocabularyRepository;
            Init();
        }

        private void Init()
        {
            foreach (var entry in _vocabularyRepository.GetAll())
            {
                if (!_words.TryAdd(entry.Term, entry))
                {
                    Console.WriteLine($"can't insert {entry.Term}");
                }
            }
            _index = _words.Count == 0 ? 1 : _words.Max(x => x.Value.TermId) + 1;
        }

        public void InsertOrUpdate(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                InsertOrUpdate(word);
            }
        }

        public int InsertOrUpdate(string word)
        {
            var term = _words.AddOrUpdate(word,
                                          key => _vocabularyRepository.Append(VocabularyEntry.Create(GetNextIndex(), key)),
                                          (key, entry) =>
                                          {
                                              entry.Occurs++;
                                              _vocabularyRepository.Update(entry.StoreId, entry.Occurs);
                                              return entry;
                                          });
            return term.TermId;
        }

        public int Get(string word)
        {
            if (_words.TryGetValue(word, out var entry))
            {
                return entry.TermId;
            }
            return -1;
        }

        public IEnumerable<VocabularyEntry> GetAll()
        {
            foreach (var term in _words)
            {
                yield return term.Value;
            }
        }
    }
}