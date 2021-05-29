using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Report;

namespace TourPlanner.ViewModels
{
    public class TourDetailViewModel : BaseViewModel
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IFileService _fileService => GetService<IFileService>();
        private IReportService _reportService => GetService<IReportService>();

        public ICommand ShowRouteCommand => new RelayCommand(ShowRoute);
        public ICommand AddLogCommand => new RelayCommand(AddLog);
        public ICommand GenerateTourReportCommand=> new RelayCommand(GenerateTourReport);
        public ICommand GenerateSummaryReportCommand=> new RelayCommand(GenerateSummaryReport);

        private void GenerateSummaryReport(object obj)
        {
            Tour tour= obj as Tour;
            _reportService.GenerateSummaryReport(tour.Name, new List<TourLog>(tour.Logs));
            MessageBox.Show("Report created", "Success", MessageBoxButton.OK);
        }

        private void GenerateTourReport(object obj)
        {
            _reportService.GenerateReport((Tour)obj);
            MessageBox.Show("Report created", "Success", MessageBoxButton.OK);
        }

        private void AddLog(object obj)
        {
            GetWindowFactory("CreateTourLogViewFactory").CreateWindow(new Dictionary<string, object> {{"tourId", (int)obj}}).Show();
        }

        private void ShowRoute(object obj)
        {
            Tour tour = (Tour) obj;
            GetWindowFactory("RouteViewFactory").CreateWindow(new Dictionary<string, object>{{"route", tour.Image}}).Show();
        }
    }
}
