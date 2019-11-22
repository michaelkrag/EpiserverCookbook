using NLPLib.Tools.Wordbook.Models;
using System.Collections.Generic;

namespace NLPLib.Tools.Wordbook
{
    public class Vocabulary : Dictionary<string, VocabularyModel>
    {
        private int _wordCounter = 0;

        public bool HasWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            return this.ContainsKey(word.ToLower());
        }

        public void Insert(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                var wordToInsert = word.ToLower();
                if (ContainsKey(wordToInsert))
                {
                    this[wordToInsert].Occurrence++;
                }
                else
                {
                    this[wordToInsert] = new VocabularyModel() { Index = _wordCounter++, Occurrence = 1 };
                }
            }
        }

        public void Insert(IEnumerable<string> wordList)
        {
            foreach (var word in wordList)
            {
                Insert(word);
            }
        }
    }
}