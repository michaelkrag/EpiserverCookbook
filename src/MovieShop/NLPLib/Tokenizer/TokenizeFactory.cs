using System.Collections.Generic;

namespace NLPLib.Tokenizer
{
    public class TokenizeFactory
    {
        private readonly Dictionary<string, string> _abbreviations;
        private readonly HashSet<string> _stopwords;

        public TokenizeFactory(Dictionary<string, string> abbreviations, HashSet<string> stopwords)
        {
            _abbreviations = abbreviations;
            _stopwords = stopwords;
        }

        public SentencesTokenize CreateSentencesTokenize()
        {
            return new SentencesTokenize(_abbreviations, _stopwords);
        }

        public WordTokenize CreateWordTokenize()
        {
            return new WordTokenize(_abbreviations, _stopwords);
        }
    }
}