using NLPLib.Search.Index.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NLPLib.Search.Index
{
    public class InvertedIndex
    {
        public ConcurrentDictionary<int, PostingList> _invertedIndex = new ConcurrentDictionary<int, PostingList>();

        public void Insert(int termId, int documentId, TermInformation termInformation)
        {
            var term = _invertedIndex.GetOrAdd(termId, new PostingList());
            term.AddDocumentIndex(documentId, termInformation);
        }

        public DocumentHit[] Search(int termId)
        {
            if (_invertedIndex.TryGetValue(termId, out var documents))
            {
                return documents.Postings.Select(x => new DocumentHit() { DocumentId = x.Key, Offsets = x.Value.TermOffsetList.Select(y => y.Key) }).ToArray();
            }
            return Array.Empty<DocumentHit>();
        }
    }
}