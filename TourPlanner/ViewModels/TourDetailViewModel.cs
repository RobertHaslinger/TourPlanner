using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels
{
    public class TourDetailViewModel : BaseViewModel
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IFileService _fileService => GetService<IFileService>();

        public ICommand ShowRouteCommand => new RelayCommand(ShowRoute);

        private void ShowRoute(object obj)
        {
            Tour tour = (Tour) obj;
            GetWindowFactory("RouteViewFactory").CreateWindow(new Dictionary<string, object>{{"route", tour.Image}}).Show();
        }
    }
}
