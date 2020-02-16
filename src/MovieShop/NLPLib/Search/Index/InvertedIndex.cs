using NLPLib.Search.Index.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Search.Index
{
    public class InvertedIndex
    {
        private ConcurrentDictionary<int, TermInfomation> _invertedIndex = new ConcurrentDictionary<int, TermInfomation>();
        private int _numberOfDocument { get; set; } = 0;

        public InvertedIndex()
        {
        }

        public void Insert(int termId, int documentId, int termOffser)
        {
            var term = _invertedIndex.GetOrAdd(termId, new TermInfomation());
            term.AddDocumentIndex(documentId, termOffser);
            _numberOfDocument = _invertedIndex.Count();
        }

        public DocumentHit[] Search(int termId)
        {
            if (_invertedIndex.TryGetValue(termId, out var documents))
            {
                return documents.Documents.Select(x => new DocumentHit() { DocumentId = x.Key, Offsets = x.Value.TermOffsetList }).ToArray();
            }
            return Array.Empty<DocumentHit>();
        }
    }
}