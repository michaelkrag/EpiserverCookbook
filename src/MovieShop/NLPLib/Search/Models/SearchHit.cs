using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Search.Models
{
    public class SearchHit<TObj>
    {
        public TObj Document { get; set; }
        public double Score { get; set; }

        public override string ToString()
        {
            return $"{Score}";
        }
    }
}