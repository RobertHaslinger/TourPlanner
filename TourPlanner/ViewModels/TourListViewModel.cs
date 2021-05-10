using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels
{
    public class TourListViewModel : BaseViewModel
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IFileService _fileService => GetService<IFileService>();

        public TourListViewModel()
        {
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
            throw new NotImplementedException();
        }

        public void UpdateTours()
        {
            Tours = new ObservableCollection<Tour>(_databaseService.GetTours());_databaseService.GetTours();
            foreach (Tour tour in Tours)
            {
                tour.LoadImage(_fileService);
            }
        }
    }
}
