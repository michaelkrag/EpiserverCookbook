namespace SuggestionApi.NLP.Gram.Models
{
    public class ContextWord
    {
        public string Word { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Word} : {Count}";
        }
    }
}