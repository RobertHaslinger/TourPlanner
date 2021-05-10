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
        void DeleteTour(int tourId);
        void EditTour(Tour editedTour);
        List<Tour> GetTours();
    }
}
