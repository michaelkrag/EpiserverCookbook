using System.Collections.Generic;

namespace NLPLib.Search.Index.Models
{
    public class DocumentInformation
    {
        public List<int> TermOffsetList { get; set; } = new List<int>();

        public int NumberOfTerms { get; private set; }

        public void AddOffset(int offset)
        {
            NumberOfTerms++;
            TermOffsetList.Add(offset);
        }
    }
}