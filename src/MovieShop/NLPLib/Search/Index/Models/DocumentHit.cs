using System.Collections.Generic;

namespace NLPLib.Search.Index.Models
{
    public class DocumentHit
    {
        public int DocumentId { get; set; }
        public IEnumerable<int> Offsets { get; set; } = new List<int>();
    }
}