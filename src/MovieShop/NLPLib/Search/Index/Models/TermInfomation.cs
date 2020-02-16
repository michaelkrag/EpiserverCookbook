using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Search.Index.Models
{
    public class TermInfomation
    {
        public ConcurrentDictionary<int, DocumentInformation> Documents { get; set; } = new ConcurrentDictionary<int, DocumentInformation>();
        public int NumberOfTerms { get; private set; } = 0;
        public int NumberOfDocuments { get; private set; } = 0;

        public void AddDocumentIndex(int documentId, int termOffset)
        {
            NumberOfTerms++;
            var document = Documents.GetOrAdd(documentId, new DocumentInformation());
            document.AddOffset(termOffset);
            NumberOfDocuments = Documents.Count();
        }
    }
}