using EPiServer;
using EPiServer.Core;
using NLPLib.Search.DocumentStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieShop.Adapters.DocumentStore
{
    public class DocumentStore : IDocumentStorage
    {
        private readonly IContentLoader _contentLoader;

        private Dictionary<int, object> _storage = new Dictionary<int, object>();

        public DocumentStore(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public virtual int Insert<TObj>(int documentID, TObj obj)
        {
            _storage[documentID] = obj;
            return documentID;
        }

        public virtual TObj Get<TObj>(int id)
        {
            if (_storage.TryGetValue(id, out var value))
            {
                return (TObj)value;
            }

            var contentReference = new ContentReference(id);

            var content = _contentLoader.Get<ContentData>(contentReference);

            return (TObj)_storage[id];
        }
    }
}