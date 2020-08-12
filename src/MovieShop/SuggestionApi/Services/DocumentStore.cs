using LiteDB;
using SuggestionApi.Models.Indexing;
using System;
using System.IO;

namespace SuggestionApi.Services
{
    public class DocumentStore : IDisposable
    {
        private readonly LiteDatabase _liteDatabase;

        public DocumentStore(string path, string index)
        {
            var indexPath = $"{path}\\{index}";
            var directoryInfo = Directory.CreateDirectory(indexPath);
            _liteDatabase = new LiteDatabase($"{indexPath}\\ds.db");
        }

        public void Dispose()
        {
            _liteDatabase.Dispose();
        }

        public int InsertOrUpdate(Doc doc)
        {
            return 0;
        }
    }
}