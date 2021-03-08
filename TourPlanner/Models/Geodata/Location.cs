using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.Geodata
{
    //TODO add RecordType Enum, Properties and many more...
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public GeoLocation GeoLocation { get; set; }
    }
}
