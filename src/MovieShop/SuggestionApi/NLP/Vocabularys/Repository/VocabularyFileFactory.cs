using SuggestionApi.Infrastructor.FileHelper;
using SuggestionApi.NLP.Vocabularys.Models;
using SuggestionApi.Services;

namespace SuggestionApi.NLP.Vocabularys.Repository
{
    public class VocabularyFileFactory : IVocabularyFileFactory
    {
        private readonly IFileLocation _fileLocation;
        private const string FileName = "_vocabulary.bin";

        public VocabularyFileFactory(IFileLocation fileLocation)
        {
            _fileLocation = fileLocation;
        }

        public IVocabularyRepository Get(string index)
        {
            var file = _fileLocation.Get(index, FileName);
            return new VocabularyFileRepository(new FileRepository<FileEntry>(file));
        }
    }
}