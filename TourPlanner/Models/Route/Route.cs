using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.Route
{
    public class Route
    {
        public bool HasTollRoad { get; set; }
        public bool HasFerry { get; set; }
        public bool HasSeasonalClosure { get; set; }
        public bool HasHighway { get; set; }
        public bool HasUnpaved { get; set; }
        public bool HasCountryCross { get; set; }
        public long EstimatedRouteTime { get; set; }
        public string EstimatedFormattedRouteTime { get; set; }
        public double Distance { get; set; }
        public double FuelUsed { get; set; }
        
    }
}
