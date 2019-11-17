using System.Collections.Generic;

namespace NLPLib.Tools.Wordbook
{
    public class Vocabulary : Dictionary<string, int>
    {
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
                    this[wordToInsert]++;
                }
                else
                {
                    this[wordToInsert] = 1;
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