using System.Collections.Generic;

namespace MovieShop.Business.Services.Search
{
    public interface IAutocompleateService
    {
        IEnumerable<string> GetSuggestions(string query);
    }
}