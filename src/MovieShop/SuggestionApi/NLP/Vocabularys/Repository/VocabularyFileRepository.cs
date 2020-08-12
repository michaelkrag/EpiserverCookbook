using SuggestionApi.Infrastructor.FileHelper;
using SuggestionApi.NLP.Vocabularys.Models;
using System.Collections.Generic;

namespace SuggestionApi.NLP.Vocabularys
{
    public class VocabularyFileRepository : IVocabularyRepository
    {
        private readonly FileRepository<FileEntry> _fileRepository;

        public VocabularyFileRepository(FileRepository<FileEntry> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public IEnumerable<VocabularyEntry> GetAll()
        {
            foreach (var entry in _fileRepository.GetAll())
            {
                yield return new VocabularyEntry() { Occurs = entry.Occurs, StoreId = entry.Position, Term = entry.Term, TermId = entry.TermId };
            }
        }

        public void Update(long storeId, int occurs)
        {
            _fileRepository.Update(occurs, storeId, x => x.Occurs);
        }

        public VocabularyEntry Append(VocabularyEntry vocabularyEntry)
        {
            var entry = _fileRepository.Append(new FileEntry() { Occurs = vocabularyEntry.Occurs, TermId = vocabularyEntry.TermId, Term = vocabularyEntry.Term });
            return new VocabularyEntry() { Occurs = entry.Occurs, StoreId = entry.Position, Term = entry.Term, TermId = entry.TermId };
        }
    }
}