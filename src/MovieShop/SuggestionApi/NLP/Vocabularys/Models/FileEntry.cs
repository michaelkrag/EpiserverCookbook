using SuggestionApi.Infrastructor.FileHelper.Attributes;

namespace SuggestionApi.NLP.Vocabularys.Models
{
    public class FileEntry
    {
        [FilePosition(1)]
        public int TermId { get; set; }

        [FilePosition(2)]
        public int Occurs { get; set; }

        [FilePosition(3)]
        public string Term { get; set; }

        public long Position { get; set; }

        public override string ToString()
        {
            return $"{TermId}: {Term} -StoreId: {Position}";
        }
    }
}