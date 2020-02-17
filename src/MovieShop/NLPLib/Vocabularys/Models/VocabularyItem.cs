using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Vocabularys.Models
{
    public class VocabularyItem
    {
        public string Term { get; set; }
        public int TermId { get; set; }
        public int Occurs { get; set; }
    }
}