using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuggestionApi.Models;

namespace SuggestionApi.Services
{
    public class Tokinizer
    {
        private static IEnumerable<Token> Tokens(string text, StopChars stopChars)
        {
            var stringBuilder = new StringBuilder();
            var index = 0;
            foreach ((var ch, int i) in text.Select((value, i) => (value, i)))
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
                else if (stopChars.IsAStopChar(ch))
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

        public static IEnumerable<Token> GetTokens(string text, HashSet<string> stopWords = null, StopChars stopChars = null)
        {
            stopWords = stopWords ?? new HashSet<string>();
            stopChars = stopChars ?? new StopChars();

            foreach (var token in Tokens(text, stopChars))
            {
                if (!stopWords.Contains(token.Term))
                {
                    yield return token;
                }
            }
        }
    }
}