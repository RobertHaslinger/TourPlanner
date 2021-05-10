using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourPlanner.DataAccessLayer.Models.Geodata
{
    public class JsonLocation : IJsonModel<Location>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayString")]
        public string DisplayString { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("place")]
        public JsonPlace Place { get; set; }


        public Location GetModel()
        {
            return new Location()
            {
                DisplayName = DisplayString,
                Id = Id,
                Name = Name,
                Place = Place==null ? new Place() : Place.GetModel()
            };
        }
    }
}
