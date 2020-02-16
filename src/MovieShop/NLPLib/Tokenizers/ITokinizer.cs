using NLPLib.Tokenizers.Models;
using System.Collections.Generic;

namespace NLPLib.Tokenizers
{
    public interface ITokinizer
    {
        IEnumerable<Token> GetTokens(string text);
    }
}