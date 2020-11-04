using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuggestionApi.InformationRetrieval
{
    public class SearchRespository : ISearchRespository
    {
        public Search Get(string index, ISearchVocabulary searchVocabulary)
        {
            return new Search();
        }

        public void Set(string index, Search vocabulary)
        {
        }
    }
}