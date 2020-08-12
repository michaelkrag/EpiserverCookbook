using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuggestionApi.Models.Indexing
{
    public class Field
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return $"{{{name} : {type} : {value}}}";
        }
    }
}