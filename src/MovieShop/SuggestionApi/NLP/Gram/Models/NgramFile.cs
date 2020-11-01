using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuggestionApi.NLP.Gram.Models
{
    public class NgramFile
    {
        public int N { get; set; }
        public IEnumerable<GramEntry> Entryes { get; set; }
    }
}