using NLPLib.Tokenizers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLPLib.Tokenizers
{
    public class Tokinizer : ITokinizer
    {
        private HashSet<string> _stopWords;

        public Tokinizer(HashSet<string> stopWords)
        {
            _stopWords = stopWords ?? new HashSet<string>();
        }

        private IEnumerable<Token> Tokens(string text)
        {
            var stringBuilder = new StringBuilder();
            var index = 0;
            foreach ((var ch, int i) in text.Select((value, i) => (value, i)))
            {
                if (ch == ' ' || ch == ',' || ch == '-')
                {
                    if (stringBuilder.Length > 0)
                    {
                        yield return Token.Create(stringBuilder.ToString().ToLower(), index);
                        stringBuilder.Clear();
                    }
                    index = i + 1;
                }
                else if (ch == '!' || ch == '?' || ch == '.')
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

        public IEnumerable<Token> GetTokens(string text)
        {
            foreach (var token in Tokens(text))
            {
                if (!_stopWords.Contains(token.Term))
                {
                    yield return token;
                }
            }
        }
    }
}