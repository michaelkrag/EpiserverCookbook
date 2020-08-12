using Newtonsoft.Json;

namespace MoveDb.Models
{
    public partial class Person
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}