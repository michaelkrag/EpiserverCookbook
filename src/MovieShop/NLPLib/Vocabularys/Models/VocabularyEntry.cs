using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Vocabularys.Models
{
    public class VocabularyEntry
    {
        public int TermId { get; private set; }
        public int Occurs { get; private set; } = 0;

        public VocabularyEntry(int termId)
        {
            TermId = termId;
        }

        public void Occurred()
        {
            Occurs++;
        }

        public override string ToString()
        {
            return $"{TermId} : {Occurs}";
        }
    }
}