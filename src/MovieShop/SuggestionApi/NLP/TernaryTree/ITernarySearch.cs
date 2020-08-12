using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuggestionApi.NLP.TernaryTree
{
    public interface ITernarySearch
    {
        void Insert(string word, int occurrence);

        IEnumerable<string> Compleate(string word);
    }
}