namespace SuggestionApi.InformationRetrieval
{
    public interface ISearchRespository
    {
        Search Get(string index, ISearchVocabulary searchVocabulary);

        void Set(string index, Search vocabulary);
    }
}