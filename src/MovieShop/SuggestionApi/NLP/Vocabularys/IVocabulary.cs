using SuggestionApi.NLP.Vocabularys.Models;
using System.Collections.Generic;

namespace SuggestionApi.NLP.Vocabularys
{
    public interface IVocabulary
    {
        int InsertOrUpdate(string word);

        void InsertOrUpdate(IEnumerable<string> words);

        int Get(string word);

        IEnumerable<VocabularyEntry> GetAll();
    }
}