using NLPLib.Vocabularys.Models;
using System.Collections.Generic;

namespace NLPLib.Vocabularys
{
    public interface IVocabulary : IEnumerable<WordInfo>
    {
        int GetIndex(string word);

        int GetOrAddIndex(string word);
    }
}