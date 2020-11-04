using LiteDB;
using SuggestionApi.Infrastructor.Document.Models;
using System;

namespace SuggestionApi.Infrastructor.Document
{
    public class DocumentStore : IDisposable
    {
        private readonly LiteDatabase _liteDatabase;

        public DocumentStore(string path)
        {
            _liteDatabase = new LiteDatabase(path);
        }

        public void Dispose()
        {
            _liteDatabase.Dispose();
        }

        public int Insert(string docId, string sentence)
        {
            var docStore = _liteDatabase.GetCollection<DbDocument>("sentence");

            var doc = new DbDocument()
            {
                Sentence = sentence,
                DocumentId = docId
            };
            var id = docStore.Insert(doc);
            return id;
        }
    }
}