using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourPlanner.Models.Route
{
    public class JsonFullRoute : IJsonModel<Route>
    {
        [JsonProperty("route")]
        public JsonRoute JsonRoute { get; set; }

        public Route GetModel()
        {
            return JsonRoute.GetModel();
        }
    }
}
