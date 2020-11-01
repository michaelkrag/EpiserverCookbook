using System.Collections.Generic;

namespace SuggestionApi.NLP.Gram.Models
{
    public class GramEntry
    {
        public string Word { get; set; }
        public IEnumerable<ContextWord> ContextWords { get; set; }
    }
}