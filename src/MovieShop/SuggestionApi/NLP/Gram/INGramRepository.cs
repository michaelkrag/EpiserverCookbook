using SuggestionApi.NLP.Gram.Models;
using System.Collections.Generic;

namespace SuggestionApi.NLP.Gram
{
    public interface INGramRepository
    {
        NGram Get(string index, int n);

        void Set(string index, NGram gramEntries);
    }
}