using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Tokenizers
{
    public class Sentencezer : ISentencezer
    {
        private readonly ITokinizer _tokinizer;

        public Sentencezer(ITokinizer tokinizer)
        {
            _tokinizer = tokinizer;
        }

        public IEnumerable<string[]> GetSentenc(string corpus)
        {
            var sentenc = new List<string>();
            foreach (var token in _tokinizer.GetTokens(corpus))
            {
                if (token.Term == "." || token.Term == "?" || token.Term == "!")
                {
                    yield return sentenc.ToArray();
                    sentenc.Clear();
                }
                else
                {
                    sentenc.Add(token.Term);
                }
            }
            if (sentenc.Count > 0)
            {
                yield return sentenc.ToArray();
            }
        }
    }
}
