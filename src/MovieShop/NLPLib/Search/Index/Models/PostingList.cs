using System.Collections.Concurrent;
using System.Linq;

namespace NLPLib.Search.Index.Models
{
    public class PostingList
    {
        public ConcurrentDictionary<int, Posting> Postings { get; set; } = new ConcurrentDictionary<int, Posting>();
        public int NumberOfTerms { get; private set; } = 0;
        public int NumberOfDocuments { get; private set; } = 0;

        public void AddDocumentIndex(int documentId, TermInformation termInformation)
        {
            NumberOfTerms++;
            var document = Postings.GetOrAdd(documentId, new Posting());
            document.AddOffset(termInformation);
            NumberOfDocuments = Postings.Count();
        }
    }
}