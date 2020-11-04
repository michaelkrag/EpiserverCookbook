using Newtonsoft.Json;
using SuggestionApi.NLP.Vocabularys.Models;
using SuggestionApi.Services;
using System.Collections.Concurrent;
using System.IO;

namespace SuggestionApi.NLP.Vocabularys
{
    public class VocabularyRepository : IVocabularyRepository
    {
        private static ConcurrentDictionary<string, Vocabulary> _vocabularys = new ConcurrentDictionary<string, Vocabulary>();
        private readonly IFileLocation _fileLocation;

        public VocabularyRepository(IFileLocation fileLocation)
        {
            _fileLocation = fileLocation;
        }

        private string GetFileName(string index) => $"{_fileLocation.GetBasePath(index)}//Vocabulary.json";

        public Vocabulary Get(string index)
        {
            return _vocabularys.GetOrAdd($"{GetFileName(index)}", x => Load(index));
        }

        private Vocabulary Load(string index)
        {
            var path = GetFileName(index);
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                var file = JsonConvert.DeserializeObject<VocabularyFile>(text);
                return new Vocabulary(file);
            }
            return new Vocabulary();
        }

        public void Set(string index, Vocabulary vocabulary)
        {
            var path = GetFileName(index);
            var obj = new VocabularyFile()
            {
                NumberOfWords = vocabulary.NumberOfWords,
                VocabularyEntries = vocabulary.GetAll()
            };

            var jsonText = JsonConvert.SerializeObject(obj);

            File.WriteAllText(path, jsonText);
            _vocabularys[index] = vocabulary;
        }
    }
}