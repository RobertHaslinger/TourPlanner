using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourPlanner.DataAccessLayer.Models.Geodata
{
    public class JsonPlace : IJsonModel<Place>
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("geometry")]
        public JsonGeometry Geometry { get; set; }

        public Place GetModel()
        {
            return new Place()
            {
                Type = Type,
                GeoLocation = Geometry==null ? new GeoLocation() : Geometry.GetModel()
            };
        }
    }
}
