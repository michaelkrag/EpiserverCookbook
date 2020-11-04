namespace SuggestionApi.NLP.Vocabularys
{
    public interface IVocabularyRepository
    {
        Vocabulary Get(string index);

        void Set(string index, Vocabulary vocabulary);
    }
}