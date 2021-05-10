using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccessLayer.Models.Geodata;
using TourPlanner.DataAccessLayer.Models.Route;

namespace TourPlanner.Services.Direction
{
    public interface IDirectionService
    {
        Task<Route> GetSimpleRoute(Location start, Location end);
    }
}
