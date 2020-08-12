namespace SuggestionApi.NLP.TernaryTree
{
    public interface ITernarySearchFactory
    {
        ITernarySearch Get(string index);
    }
}