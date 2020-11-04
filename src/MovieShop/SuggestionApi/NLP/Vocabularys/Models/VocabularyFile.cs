using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuggestionApi.NLP.Vocabularys.Models
{
    public class VocabularyFile
    {
        public int NumberOfWords { get; set; }
        public IEnumerable<VocabularyEntry> VocabularyEntries { get; set; }
    }
}