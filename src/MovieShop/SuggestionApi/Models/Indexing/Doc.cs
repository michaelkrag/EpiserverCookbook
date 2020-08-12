using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuggestionApi.Models.Indexing
{
    public class Doc
    {
        public string id { get; set; }
        public List<Field> fields { get; set; }

        public override string ToString()
        {
            return $"{id}";
        }
    }
}