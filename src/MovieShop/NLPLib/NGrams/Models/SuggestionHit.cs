using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.NGrams.Models
{
    public class SuggestionHit
    {
        public string term { get; set; }
        public double Score { get; set; }
    }
}