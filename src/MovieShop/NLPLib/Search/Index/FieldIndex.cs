using System.Collections.Concurrent;

namespace NLPLib.Search.Index
{
    public class FieldIndex
    {
        public ConcurrentDictionary<string, InvertedIndex> _fieldIndex = new ConcurrentDictionary<string, InvertedIndex>();

        public InvertedIndex GetIndexer(string fieldName)
        {
            var i = _fieldIndex.GetOrAdd(fieldName, x => new InvertedIndex());
            return i;
        }
    }
}