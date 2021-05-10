using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using TourPlanner.DataAccessLayer.Models.Geodata;
using TourPlanner.Helper;

namespace TourPlanner.Services.Map
{
    public class MapQuestMapService : MapQuestHttpBase, IMapService
    {
        public async Task<BitmapImage> GetMapWithLocations(Location start, Location end)
        {
            if (start==null || end==null) return null;
            try
            {
                string uri =
                    $"staticmap/v5/map?start={start.Name}&end={end.Name}&size=600,400&format=png&key={ConfigurationManager.AppSettings["consumer_key"]}";
                using var response = await HttpClient.GetAsync(uri);
                BitmapImage image = HelperBase.LoadImage(await response.Content.ReadAsByteArrayAsync());
                return image;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
