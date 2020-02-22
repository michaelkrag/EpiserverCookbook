using NLPLib.Search.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Search.Models
{
    public class SearchExport
    {
        public InvertedIndex InvertedIndex { get; set; }
        public Dictionary<int, int> DocumentNumberOfTerms { get; set; }
        public int NumberOfDocuments { get; set; }
        public int NumberOfTerms { get; set; }
    }
}