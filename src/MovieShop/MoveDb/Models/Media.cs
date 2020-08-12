using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveDb.Models
{
    public partial class Media
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("episodes")]
        public object[] Episodes { get; set; }

        [JsonProperty("seasons")]
        public Season[] Seasons { get; set; }
    }
}