using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SuggestionApi.Services
{
    public class FileLocation : IFileLocation
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileLocation(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetBasePath(string index)
        {
            var path = $"{_webHostEnvironment.ContentRootPath}//Data//{index}";
            Directory.CreateDirectory(path);
            return path;
        }

        public string Get(string index, string fileName)
        {
            return $"{GetBasePath(index)}//{fileName}";
        }
    }
}