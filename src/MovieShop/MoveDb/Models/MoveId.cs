using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveDb.Models
{
    public class MoveId
    {
        public bool adult { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public double popularity { get; set; }
        public bool video { get; set; }
    }
}