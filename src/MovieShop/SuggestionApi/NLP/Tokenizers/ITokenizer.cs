using SuggestionApi.NLP.Tokenizers.Models;
using System.Collections.Generic;

namespace SuggestionApi.NLP.Tokenizers
{
    public interface ITokenizer
    {
        IEnumerable<Sentence> Parse(string corpus);
    }
}