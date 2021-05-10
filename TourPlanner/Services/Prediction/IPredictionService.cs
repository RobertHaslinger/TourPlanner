using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Models.Geodata;

namespace TourPlanner.Services.Prediction
{
    public interface IPredictionService
    {
        Task<List<Location>> FetchPredictions(string query);
    }
}
