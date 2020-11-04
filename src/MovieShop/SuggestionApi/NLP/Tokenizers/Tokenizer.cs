using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
using SuggestionApi.NLP.Tokenizers.Models;
using System.Collections.Generic;
using System.Linq;

namespace SuggestionApi.NLP.Tokenizers
{
    public class Tokenizer : ITokenizer
    {
        private static string modelPath = "c:\\temp\\EnglishSD.nbin";
        private static string modelTokenPath = "c:\\temp\\EnglishTok.nbin";
        private static HashSet<string> specialTokens = new HashSet<string>() { "-", "!", "?", ".", "\\", "(", ")", ":", ";", "," };

        private readonly EnglishMaximumEntropySentenceDetector _sentenceDetector;
        private readonly EnglishMaximumEntropyTokenizer _tokenizer;

        public Tokenizer()
        {
            _sentenceDetector = new EnglishMaximumEntropySentenceDetector(modelPath);
            _tokenizer = new EnglishMaximumEntropyTokenizer(modelTokenPath);
        }

        public IEnumerable<Sentence> Parse(string corpus)
        {
            var sentences = _sentenceDetector.SentenceDetect(corpus);
            foreach (var sentence in sentences)
            {
                var tokens = _tokenizer.Tokenize(sentence).Where(x => !specialTokens.Contains(x)).Select(x => x.ToLower()).ToArray();
                yield return Sentence.Create(sentence, tokens);
            }
        }
    }
}