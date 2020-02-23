using NLPLib.NGrams.Models;
using System.Collections.Generic;

namespace NLPLib.NGrams
{
    public interface INGram
    {
        IEnumerable<SuggestionHit> GetSuggestions(string word);
    }
}