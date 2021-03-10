using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourPlanner.Models.Geodata
{
    public class JsonLocationArray : IJsonModel<IEnumerable<Location>>
    {
        [JsonProperty("results")]
        public JsonLocation[] Results { get; set; } = new JsonLocation[0];

        public IEnumerable<Location> GetModel()
        {
            return Results.Select(x => x.GetModel());
        }
    }
}
