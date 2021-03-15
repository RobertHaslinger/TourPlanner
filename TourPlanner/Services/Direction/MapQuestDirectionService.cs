using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using TourPlanner.Helper;
using TourPlanner.Models.Geodata;
using TourPlanner.Models.Route;

namespace TourPlanner.Services.Direction
{
    public class MapQuestDirectionService : MapQuestHttpBase, IDirectionService
    {
        public async Task<Route> GetSimpleRoute(Location start, Location end)
        {
            if (start == null || end == null) return null;
            try
            {
                string uri =
                    $"directions/v2/route?from={start.Name}&to={end.Name}&key={ConfigurationManager.AppSettings["consumer_key"]}";
                using var response = await HttpClient.GetAsync(uri);
                return JsonConvert.DeserializeObject<JsonFullRoute>(await response.Content.ReadAsStringAsync()).GetModel();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
