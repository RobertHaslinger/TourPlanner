using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.Geodata
{
    public class Place
    {
        public string Type { get; set; }
        public GeoLocation GeoLocation { get; set; } = new GeoLocation();
    }
}
