using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuggestionApi.NLP.Tokenizers.Models
{
    public class Sentence
    {
        public string Word { get; private set; }
        public string[] Tokens { get; private set; }

        public static Sentence Create(string word, string[] tokens)
        {
            return new Sentence()
            {
                Word = word,
                Tokens = tokens
            };
        }
    }
}