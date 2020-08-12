using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveDb.Models
{
    public partial class Credit
    {
        [JsonProperty("credit_type")]
        public string CreditType { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}