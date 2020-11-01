using Newtonsoft.Json;
using SuggestionApi.NLP.Gram.Models;
using SuggestionApi.Services;
using System.Collections.Concurrent;
using System.IO;

namespace SuggestionApi.NLP.Gram
{
    public class NGramRepository : INGramRepository
    {
        private readonly IFileLocation _fileLocation;
        private static ConcurrentDictionary<string, NGram> _nGrams = new ConcurrentDictionary<string, NGram>();

        public NGramRepository(IFileLocation fileLocation)
        {
            _fileLocation = fileLocation;
        }

        private string GetFileName(string index, int n) => $"{_fileLocation.GetBasePath(index)}//{n}gram.json";

        private string GetKey(string index, int n) => $"{index}{n}";

        public NGram Get(string index, int n)
        {
            return _nGrams.GetOrAdd($"{GetKey(index, n)}", x => Load(index, n));
        }

        private NGram Load(string index, int n)
        {
            var path = GetFileName(index, n);
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<NgramFile>(text);
                return new NGram(file.Entryes, n);
            }
            return new NGram(n);
        }

        public void Set(string index, NGram gramEntries)
        {
            var path = GetFileName(index, gramEntries.Size);
            var obj = new NgramFile()
            {
                Entryes = gramEntries,
                N = gramEntries.Size
            };

            var jsonText = JsonConvert.SerializeObject(obj);

            File.WriteAllText(path, jsonText);
            _nGrams[GetKey(index, gramEntries.Size)] = gramEntries;
        }
    }
}