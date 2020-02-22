using System.Collections.Generic;
using System.Linq;

namespace NLPLib.Search.DocumentStores
{
    public class DocumentStorageMemory : IDocumentStorage
    {
        private Dictionary<int, object> _storage = new Dictionary<int, object>();

        public virtual int Insert<TObj>(int documentID, TObj obj) where TObj : class
        {
            _storage[documentID] = obj;
            return documentID;
        }

        public virtual TObj Get<TObj>(int id) where TObj : class
        {
            return (TObj)_storage[id];
        }

        public int NumberOfDocuments => _storage.Count();
    }
}