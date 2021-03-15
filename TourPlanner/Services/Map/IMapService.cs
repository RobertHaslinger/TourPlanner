using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourPlanner.Models.Geodata;

namespace TourPlanner.Services.Map
{
    public interface IMapService
    {
        Task<BitmapImage> GetMapWithLocations(Location start, Location end);
    }
}
