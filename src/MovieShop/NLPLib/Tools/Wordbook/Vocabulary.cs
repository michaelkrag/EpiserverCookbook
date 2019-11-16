using System.Collections.Generic;

namespace NLPLib.Tools.Wordbook
{
    public class Vocabulary : Dictionary<string, int>
    {
        public void Insert(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                if (ContainsKey(word))
                {
                    this[word]++;
                }
                else
                {
                    this[word] = 1;
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