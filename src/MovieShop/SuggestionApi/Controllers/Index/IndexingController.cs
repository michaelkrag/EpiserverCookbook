using Microsoft.AspNetCore.Mvc;
using SuggestionApi.Models.Indexing;
using SuggestionApi.NLP.Gram;
using SuggestionApi.NLP.TernaryTree;
using SuggestionApi.NLP.Vocabularys;
using SuggestionApi.NLP.Vocabularys.Models;

using SuggestionApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SuggestionApi.Controllers.Index
{
    [ApiController]
    [Route("[controller]")]
    public class IndexingController : ControllerBase
    {
        private readonly IndexService _indexService;
        private readonly IFileLocation _fileLocation;
        private readonly IVocabularyRepository _vocabularyRepository;

        public IndexingController(IndexService indexService, IFileLocation fileLocation, IVocabularyRepository vocabularyRepository)
        {
            _indexService = indexService;
            _fileLocation = fileLocation;
            _vocabularyRepository = vocabularyRepository;
        }

        [HttpPost("{index}")]
        public int Post(string index, [FromBody] IEnumerable<Doc> texts)
        {
            return _indexService.Indexing(index, texts);
        }

        [HttpGet("{index}")]
        public IEnumerable<VocabularyEntry> Get(string index)
        {
            var vocabulary = _vocabularyRepository.Get(index);
            var tokens = vocabulary.GetAll().OrderByDescending(x => x.Occurs).ToList();
            return tokens;
        }

        [HttpDelete("{index}")]
        public void Delete(string index)
        {
            var targetDirectory = _fileLocation.GetBasePath(index);

            foreach (var file in Directory.GetFiles(targetDirectory))
            {
                System.IO.File.Delete(file);
            }
            Directory.Delete(targetDirectory);
        }
    }
}