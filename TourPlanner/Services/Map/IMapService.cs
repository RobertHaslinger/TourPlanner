using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourPlanner.DataAccessLayer.Models.Geodata;

namespace TourPlanner.Services.Map
{
    public interface IMapService
    {
        Task<BitmapImage> GetMapWithLocations(Location start, Location end);
        Task<BitmapImage> GetMapWithStrings(string start, string end);
    }
}
