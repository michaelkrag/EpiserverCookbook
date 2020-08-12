using Newtonsoft.Json;
using System.Collections.Generic;

namespace SuggestionApi.Services
{
    public class StopChars
    {
        private readonly HashSet<char> _stopChars = new HashSet<char>();

        public StopChars()
        {
        }

        public StopChars(IEnumerable<char> stopchars)
        {
            _stopChars = Setup(stopchars);
        }

        public StopChars(string fileName)
        {
            var json = System.IO.File.ReadAllText(fileName);
            var stopchars = JsonConvert.DeserializeObject<IEnumerable<char>>(json);
            _stopChars = Setup(stopchars);
        }

        private static HashSet<char> Setup(IEnumerable<char> stopchars)
        {
            HashSet<char> hashSet = new HashSet<char>();
            foreach (var c in stopchars)
            {
                if (!hashSet.Contains(c))
                {
                    hashSet.Add(c);
                }
            }
            return hashSet;
        }

        public bool IsAStopChar(char c)
        {
            return _stopChars.Contains(c);
        }
    }
}