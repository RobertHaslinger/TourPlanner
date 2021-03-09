using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TourPlanner.Models.Geodata
{
    //TODO add RecordType Enum, Properties and many more...
    public class Location
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("displayString")]
        public string DisplayName { get; set; }

        //TODO Location
        public GeoLocation GeoLocation { get; set; }
    }
}
