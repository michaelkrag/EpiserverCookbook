using Microsoft.AspNetCore.Mvc;
using SuggestionApi.Models.Indexing;
using SuggestionApi.NLP.Gram;
using SuggestionApi.NLP.TernaryTree;
using SuggestionApi.NLP.Vocabularys.Models;
using SuggestionApi.NLP.Vocabularys.Repository;
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
        private readonly IVocabularyFileFactory _vocabularyFileFactory;
        private readonly IFileLocation _fileLocation;

        public IndexingController(IndexService indexService, IVocabularyFileFactory vocabularyFileFactory, IFileLocation fileLocation)
        {
            _indexService = indexService;
            _vocabularyFileFactory = vocabularyFileFactory;
            _fileLocation = fileLocation;
        }

        [HttpPost("{index}")]
        public int Post(string index, [FromBody] IEnumerable<Doc> texts)
        {
            return _indexService.Indexing(index, texts);
        }

        [HttpGet("{index}")]
        public IEnumerable<VocabularyEntry> Get(string index)
        {
            var vocabulary = _vocabularyFileFactory.Get(index);
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