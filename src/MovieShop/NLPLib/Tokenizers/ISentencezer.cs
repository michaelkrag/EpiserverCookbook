using System.Collections.Generic;

namespace NLPLib.Tokenizers
{
    public interface ISentencezer
    {
        IEnumerable<string[]> GetSentenc(string corpus);
    }
}