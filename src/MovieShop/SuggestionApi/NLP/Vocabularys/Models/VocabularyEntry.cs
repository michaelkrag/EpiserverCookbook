namespace SuggestionApi.NLP.Vocabularys.Models
{
    public class VocabularyEntry
    {
        public int TermId { get; set; }
        public string Term { get; set; }
        public int Occurs { get; set; }
        public long StoreId { get; set; }

        public static VocabularyEntry Create(int termId, string term)
        {
            return new VocabularyEntry()
            {
                Occurs = 1,
                StoreId = -1,
                Term = term,
                TermId = termId
            };
        }

        public override string ToString()
        {
            return $"{TermId}: {Term} -> {Occurs}";
        }
    }
}