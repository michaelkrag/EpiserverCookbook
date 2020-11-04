using SuggestionApi.Services;

namespace SuggestionApi.Infrastructor.Document
{
    public class DocumentStoreRepository : IDocumentStoreRepository
    {
        private readonly IFileLocation _fileLocation;

        public DocumentStoreRepository(IFileLocation fileLocation)
        {
            _fileLocation = fileLocation;
        }

        private string GetFileName(string index) => $"{_fileLocation.GetBasePath(index)}//ds.db";

        public DocumentStore Get(string index)
        {
            return new DocumentStore(GetFileName(index));
        }

        public void Set(DocumentStore documentStore)
        {
            documentStore.Dispose();
        }
    }
}