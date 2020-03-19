using System.Collections.Generic;

namespace NLPLib.Search.Index.Models
{
    public class Posting
    {
        public Dictionary<int, TermInformation> TermOffsetList { get; set; } = new Dictionary<int, TermInformation>();

        public int NumberOfTerms { get; private set; }

        public void AddOffset(TermInformation termInformation)
        {
            NumberOfTerms++;
            TermOffsetList[termInformation.FieldId] = termInformation;
        }
    }

    public class TermInformation
    {
        public int TermIndex { get; set; }
        public int FieldId { get; set; }
        public int StartIndex { get; set; }
        public int StopIndex { get; set; }
    }
}