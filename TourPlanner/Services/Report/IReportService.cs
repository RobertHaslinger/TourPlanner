using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.Services.Report
{
    public interface IReportService
    {
        void GenerateReport(Tour tour);
        void GenerateSummaryReport(string tourName, List<TourLog> logs);
    }
}
