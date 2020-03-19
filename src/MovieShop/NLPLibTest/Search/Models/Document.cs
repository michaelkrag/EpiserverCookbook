using NLPLib.Search.Attributes;

namespace NLPLibTest.Search.Models
{
    public class Document
    {
        [Indexing]
        public string Title { get; set; }

        [Indexing]
        public string Body { get; set; }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}