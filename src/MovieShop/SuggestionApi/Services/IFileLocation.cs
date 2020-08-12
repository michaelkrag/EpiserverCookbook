namespace SuggestionApi.Services
{
    public interface IFileLocation
    {
        string GetBasePath(string index);

        string Get(string index, string fileName);
    }
}