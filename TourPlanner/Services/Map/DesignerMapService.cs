using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourPlanner.DataAccessLayer.Models.Geodata;

namespace TourPlanner.Services.Map
{
    public class DesignerMapService : IMapService
    {
        public async Task<BitmapImage> GetMapWithLocations(Location start, Location end)
        {
            return new BitmapImage(new Uri("/TourPlanner;component/sample_map_600x400.png", UriKind.Absolute));
        }
    }
}
