using CommonLib.Cache;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SuggestionApi.Services
{
    public class IndexRepository
    {
        private const string IndexFileName = "index_info.json";
        private readonly string _basePath;
        private readonly ICache _cache;

        public IndexRepository(IWebHostEnvironment webHostEnvironment, ICache cache)
        {
            _basePath = $"{webHostEnvironment.ContentRootPath}//Data//";
            _cache = cache;
        }

        public Index GetOrCreateIndex(string indexName)
        {
            // return _cache.GetOrCreate<Index>(indexName, () => 10, TimeSpan.FromDays(10), CacheDurationType.Sliding);
            throw new NotImplementedException();
        }

        public bool Create(string indexName)
        {
            /*     var fileInfo = FilePath.Create(_basePath, indexName, IndexFileName);
                 if (!Exists(fileInfo))
                 {
                     return false;
                 }
                 var info = Directory.CreateDirectory(fileInfo.IndexDirectory);

                 var indexFile = new IndexFile() { Name = indexName };

                 File.WriteAllText(fileInfo.IndexFilePath, JsonConvert.SerializeObject(indexFile));*/

            return true;
        }

        public bool Delete(string indexName)
        {
            /*    var fileInfo = FilePath.Create(_basePath, indexName, IndexFileName);
                if (!Directory.Exists(fileInfo.IndexDirectory))
                {
                    return false;
                }
                var files = Directory.GetFiles(fileInfo.IndexDirectory);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
                Directory.Delete(fileInfo.IndexDirectory);*/
            return true;
        }

        public bool Get(string indexName)
        {
            return true;
        }

        /*
        private bool Exists(FilePath filePath)
        {
           if (!Directory.Exists(filePath.IndexDirectory))
            {
                return false;
            }
            if (File.Exists(filePath.IndexFilePath))
            {
                return true;
            }
            return true;
        }*/

        public IEnumerable<string> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}