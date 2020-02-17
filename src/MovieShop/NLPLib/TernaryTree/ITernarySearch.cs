using System.Collections.Generic;

namespace NLPLib.TernaryTree
{
    public interface ITernarySearch
    {
        void Insert(string word, int occurrence);

        IEnumerable<string> Compleate(string word);
    }
}