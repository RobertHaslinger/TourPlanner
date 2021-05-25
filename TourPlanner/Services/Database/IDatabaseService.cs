using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.Services.Database
{
    public interface IDatabaseService
    {
        bool AddTour(Tour tour, out string tourImagePath);
        bool AddTourLog(TourLog log);
        List<TourLog> GetTourLogs(int tourId);
        void DeleteTour(int tourId);
        bool EditTour(Tour editedTour, out string tourImagePath);
        List<Tour> GetTours();
    }
}
