using EPiServer;
using EPiServer.Core;
using ImpromptuInterface;
using NLPLib.Search.DocumentStores;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public virtual int Insert<TObj>(int documentID, TObj obj) where TObj : class
        {
            _storage[documentID] = obj;
            return documentID;
        }

        public virtual TObj Get<TObj>(int id) where TObj : class
        {
            if (_storage.TryGetValue(id, out var value))
            {
                return (TObj)value;
            }

            var contentReference = new ContentReference(id, "CatalogContent");

            var content = _contentLoader.Get<ContentData>(contentReference);
            _storage[id] = Map<TObj>(content);
            return (TObj)_storage[id];
        }

        public TObj Map<TObj>(ContentData content) where TObj : class
        {
            dynamic test = new ExpandoObject();

            IDictionary<string, object> dictionary_object = test;

            //            var obj = ObjectGenerator.Generate<TObj>(ob2);

            var propertys = typeof(TObj).GetProperties();

            foreach (var property in propertys)
            {
                dictionary_object[property.Name] = GetPropValue(content, property.Name);
                /*                var value = content.GetValue(property.Name);
                                property.SetValue(obj, value, null);*/
            }

            var obj = Impromptu.ActLike<TObj>(test);

            return (TObj)obj;
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}

//ContentMapper