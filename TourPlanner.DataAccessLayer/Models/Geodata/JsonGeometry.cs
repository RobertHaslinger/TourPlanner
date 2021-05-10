using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourPlanner.DataAccessLayer.Models.Geodata
{
    public class JsonGeometry : IJsonModel<GeoLocation>
    {
        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; } = new double[2];

        public GeoLocation GetModel()
        {
            return new GeoLocation()
            {
                Latitude = Coordinates[1],
                Longitude = Coordinates[0]
            };
        }
    }
}
