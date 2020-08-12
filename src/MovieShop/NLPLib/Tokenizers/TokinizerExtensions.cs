using NLPLib.Tokenizers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLPLib.Tokenizers
{
    public static class TokinizerExtensions
    {
        public static IEnumerable<Token> Tokens(this string corpus, HashSet<char> specialTokens = null)
        {
            specialTokens = specialTokens ?? new HashSet<char>();
            var stringBuilder = new StringBuilder();
            var index = 0;
            foreach ((var ch, int i) in corpus.Select((value, i) => (value, i)))
            {
                if (ch == ' ')
                {
                    if (stringBuilder.Length > 0)
                    {
                        yield return Token.Create(stringBuilder.ToString().ToLower(), index);
                        stringBuilder.Clear();
                    }
                    index = i + 1;
                }
                else if (specialTokens.Contains(ch))
                {
                    if (stringBuilder.Length > 0)
                    {
                        yield return Token.Create(stringBuilder.ToString().ToLower(), index);
                        stringBuilder.Clear();
                    }
                    index = i + 1;
                    yield return Token.Create(ch.ToString(), index);
                }
                else
                {
                    stringBuilder.Append(ch);
                }
            }
            if (stringBuilder.Length > 0)
            {
                yield return Token.Create(stringBuilder.ToString(), index);
            }
        }

        public static IEnumerable<string> Sentenc(this string corpus, HashSet<string> abbreviations)
        {
            var lineEnder = new HashSet<char>() { '.', '?', '!' };
            var sentence = new List<string>();
            var wordBulder = new StringBuilder();
            var containsPunbkt = false;
            foreach ((var ch, int i) in corpus.Select((value, i) => (value, i)))
            {
                if (ch == ' ')
                {
                    var word = wordBulder.ToString();
                    sentence.Add(word);
                    wordBulder.Clear();
                    if (containsPunbkt)
                    {
                        if (!abbreviations.Contains(word))
                        {
                            yield return string.Join(" ", sentence);
                            sentence.Clear();
                        }
                        containsPunbkt = false;
                    }
                    continue;
                }
                else if (lineEnder.Contains(ch))
                {
                    containsPunbkt = true;
                }
                wordBulder.Append(ch);
            }
            if (wordBulder.Length > 0)
            {
                sentence.Add(wordBulder.ToString());
            }

            if (sentence.Any())
            {
                yield return string.Join(" ", sentence);
            }
        }
    }
}