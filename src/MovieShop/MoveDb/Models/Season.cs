using Newtonsoft.Json;
using System;

namespace MoveDb.Models
{
    public partial class Season
    {
        [JsonProperty("air_date")]
        public DateTimeOffset AirDate { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("season_number")]
        public long SeasonNumber { get; set; }
    }
}