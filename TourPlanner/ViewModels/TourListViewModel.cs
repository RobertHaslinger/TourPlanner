using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Helper.Observer;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels
{
    public class TourListViewModel : BaseViewModel, IObserver
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IFileService _fileService => GetService<IFileService>();

        public TourListViewModel()
        {
            BaseObserverSingleton.GetInstance.TourObservers.Add(this);
        }

        private ObservableCollection<Tour> _tours;

        public ObservableCollection<Tour> Tours
        {
            get
            {
                if (_tours == null)
                {
                    UpdateTours();
                }

                return _tours;
            }
            set
            {
                _tours = value; OnPropertyChanged();
            }
        }

        public ICommand AddTourCommand => new RelayCommand(AddTour);

        private void AddTour(object obj)
        {
            GetWindowFactory("CreateTourViewFactory").CreateWindow(new Dictionary<string, object> {{"observers", new List<IObserver> {this}}}).Show();
        }

        public void UpdateTours()
        {
            Tours = new ObservableCollection<Tour>(_databaseService.GetTours());_databaseService.GetTours();
            foreach (Tour tour in Tours)
            {
                tour.LoadImage(_fileService);
            }
        }

        public void Update(ISubject subject)
        {
            UpdateTours();
        }
    }
}
