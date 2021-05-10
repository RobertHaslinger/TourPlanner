using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourPlanner.DataAccessLayer.Models.Route
{
    [JsonObject("route")]
    public class JsonRoute : IJsonModel<Route>
    {
        [JsonProperty("hasTollRoad")]
        public bool HasTollRoad { get; set; }
        [JsonProperty("hasFerry")]
        public bool HasFerry { get; set; }
        [JsonProperty("hasSeasonalClosure")]
        public bool HasSeasonalClosure { get; set; }
        [JsonProperty("hasUnpaved")]
        public bool HasUnpaved { get; set; }
        [JsonProperty("hasHighway")]
        public bool HasHighway { get; set; }
        [JsonProperty("hasCountryCross")]
        public bool HasCountryCross { get; set; }
        [JsonProperty("realTime")]
        public long EstimatedRouteTime { get; set; }
        [JsonProperty("formattedTime")]
        public string EstimatedFormattedRouteTime { get; set; }
        [JsonProperty("distance")]
        public double Distance { get; set; }
        [JsonProperty("fuelUsed")]
        public double FuelUsed { get; set; }

        public Route GetModel()
        {
            return new Route()
            {
                Distance = Distance,
                EstimatedFormattedRouteTime = EstimatedFormattedRouteTime,
                EstimatedRouteTime = EstimatedRouteTime,
                FuelUsed = FuelUsed,
                HasCountryCross = HasCountryCross,
                HasFerry = HasFerry,
                HasHighway = HasHighway,
                HasSeasonalClosure = HasSeasonalClosure,
                HasTollRoad = HasTollRoad,
                HasUnpaved = HasUnpaved
            };
        }
    }
}
