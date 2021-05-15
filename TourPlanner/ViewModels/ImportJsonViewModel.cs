using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using TourPlanner.Helper;
using TourPlanner.Helper.Observer;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Map;

namespace TourPlanner.ViewModels
{
    public class ImportJsonViewModel : BaseViewModel, ISubject
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IMapService _mapService => GetService<IMapService>();
        private IFileService _fileService => GetService<IFileService>();
        private List<IObserver> _observers = new List<IObserver>();

        public ImportJsonViewModel()
        {
            IsImportingEnabled = true;
        }

        private string _tourJson;

        public string TourJson
        {
            get { return _tourJson; }
            set { _tourJson = value; OnPropertyChanged();}
        }

        private bool _isImportingEnabled;

        public bool IsImportingEnabled
        {
            get { return _isImportingEnabled; }
            set { _isImportingEnabled = value; OnPropertyChanged(); }
        }

        public ICommand ImportCommand => new RelayCommand(ImportJson);
        public ICommand CancelCommand => new RelayCommand(CancelImport);

        private void CancelImport(object obj)
        {
            ((Window)obj).Close();
        }

        private void ImportJson(object obj)
        {
            try
            {
                IsImportingEnabled = false;
                List<Tour> importedTours = JsonConvert.DeserializeObject<List<Tour>>(TourJson);
                importedTours?.ForEach(async (tour) =>
                {
                    if (string.IsNullOrWhiteSpace(tour.Name) || string.IsNullOrWhiteSpace(tour.StartLocation) ||
                        string.IsNullOrWhiteSpace(tour.EndLocation))
                    {
                        throw new JsonException();
                    }
                    tour.Image = await _mapService.GetMapWithStrings(tour.StartLocation, tour.EndLocation);
                    string imagePath;
                    if (_databaseService.AddTour(tour, out imagePath) && _fileService.SaveImage(imagePath, tour.Image))
                    {
                        ((Window)obj).Close();
                        Notify();
                    }
                    else
                    {
                        MessageBox.Show("There was an error, please try again.", "Failed saving", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                });
            }
            catch (JsonException e)
            {
                MessageBox.Show("Json not in valid format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IsImportingEnabled = true;
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error importing, please try again", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                IsImportingEnabled = true;
                return;
            }
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o => o.Update(this));
        }
    }
}
