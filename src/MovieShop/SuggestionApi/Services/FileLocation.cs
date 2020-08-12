using Microsoft.AspNetCore.Hosting;

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
            return $"{_webHostEnvironment.ContentRootPath}//Data//{index}";
        }

        public string Get(string index, string fileName)
        {
            return $"{GetBasePath(index)}//{fileName}";
        }
    }
}