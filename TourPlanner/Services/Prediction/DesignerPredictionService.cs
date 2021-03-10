using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models.Geodata;

namespace TourPlanner.Services.Prediction
{
    public class DesignerPredictionService : IPredictionService
    {
        public async Task<List<Location>> FetchPredictions(string query)
        {
            List<Location> locations = new List<Location>();
            await Task.Run(() =>
            {
                for (int i = 0; i < query.Length; i++)
                {
                    locations.Add(new Location()
                    {
                        DisplayName = $"Location {i}",
                        Name = $"Location {i}, 1100 Sample ZIP",
                        Id = $"sample_{i}"
                    });
                }
            });

            return locations;
        }
    }
}
