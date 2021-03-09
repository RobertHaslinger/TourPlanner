using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using TourPlanner.Models.Geodata;

namespace TourPlanner.Services.Prediction
{
    public class MapQuestPredictionService : MapQuestHttpBase, IPredictionService
    {
        public async Task<List<Location>> FetchPredictions(string query)
        {
            string uri =
                $"search/v3/prediction?key={ConfigurationManager.AppSettings["consumer_key"]}&limit=7&collection=adminArea,poi,address,category,franchise,airport&q={query}" +
                $"&location={ConfigurationManager.AppSettings["location_base"]}";
            using var response = await HttpClient.GetAsync(uri);
            //TODO add location repo for deserializing
            Location[] locations = await response.Content.ReadFromJsonAsync<Location[]>();
            return locations == null ? new List<Location>() : new List<Location>(locations);
        }
    }
}
