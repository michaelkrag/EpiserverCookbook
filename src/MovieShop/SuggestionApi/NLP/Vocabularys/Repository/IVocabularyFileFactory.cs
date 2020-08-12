namespace SuggestionApi.NLP.Vocabularys.Repository
{
    public interface IVocabularyFileFactory
    {
        IVocabularyRepository Get(string index);
    }
}