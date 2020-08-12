using NLPLib.Vocabularys.Models;
using System.Collections.Generic;

namespace NLPLib.Vocabularys
{
    public interface IVocabulary : IEnumerable<WordInfo>
    {
        int GetIndex(string word);

        int GetOrAddIndex(string word);

        void AddIndex(IEnumerable<string> words);

        void Import(IEnumerable<VocabularyItem> terms);

        IEnumerable<VocabularyItem> Export();
    }
}