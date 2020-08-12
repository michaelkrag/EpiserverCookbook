using Microsoft.AspNetCore.Hosting;
using SuggestionApi.Infrastructor.FileHelper;
using SuggestionApi.NLP.Vocabularys;
using SuggestionApi.NLP.Vocabularys.Models;

namespace SuggestionApi.Services
{
    public class IndexFactory
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexFactory(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public Index OpenIndex(string index)
        {
            var path = $"{_webHostEnvironment.ContentRootPath}//data//";
            var vorcabelary = new Vocabulary(new VocabularyFileRepository(new FileRepository<FileEntry>($"{path}\\{index}\\_vocabulary.bin")));
            var ds = new DocumentStore(path, index);
            return new Index(ds, vorcabelary);
        }
    }
}