namespace SuggestionApi.Infrastructor.Document
{
    public interface IDocumentStoreRepository
    {
        DocumentStore Get(string index);

        void Set(DocumentStore documentStore);
    }
}