using System.Collections.Generic;

namespace NLPLib.Search.Fields
{
    public class FieldDictionary
    {
        public Dictionary<string, int> _dictionary = new Dictionary<string, int>();
        private int _counter = 0;

        public int GetOrCreate(string fieldName)
        {
            if (_dictionary.TryGetValue(fieldName, out var value))
            {
                return value;
            }

            var count = ++_counter;
            _dictionary[fieldName] = count;
            return count;
        }
    }
}