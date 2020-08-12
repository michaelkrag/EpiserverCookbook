using SuggestionApi.NLP.Vocabularys.Models;
using System.Collections.Generic;

namespace SuggestionApi.NLP.Vocabularys
{
    public interface IVocabularyRepository
    {
        IEnumerable<VocabularyEntry> GetAll();

        void Update(long storeId, int Occurs);

        VocabularyEntry Append(VocabularyEntry vocabularyEntry);
    }
}