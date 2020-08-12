namespace SuggestionApi.Models
{
    public class Token
    {
        public string Term { get; private set; }
        public int Index { get; private set; }

        public Token(string term, int index)
        {
            Term = term;
            Index = index;
        }

        public static Token Create(string term, int index)
        {
            return new Token(term, index);
        }

        public override string ToString()
        {
            return $"{Term} {Index}";
        }
    }
}