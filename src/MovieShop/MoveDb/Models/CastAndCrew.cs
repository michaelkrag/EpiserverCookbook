using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveDb.Models
{
    public partial class CastAndCrew
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cast")]
        public Cast[] Cast { get; set; }

        [JsonProperty("crew")]
        public Crew[] Crew { get; set; }
    }
}