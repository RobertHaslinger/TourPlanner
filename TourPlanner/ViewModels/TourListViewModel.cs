using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public ICommand ImportJsonCommand => new RelayCommand(ImportJson);
        public ICommand ExportJsonCommand => new RelayCommand(ExportJson);
        public ICommand SearchTextChangedCommand => new RelayCommand(OnSearchTextChangedCommandExecuted);

        private void OnSearchTextChangedCommandExecuted(object obj)
        {
            string search = obj.ToString();
            UpdateTours(search);
        }

        private void ImportJson(object obj)
        {
            GetWindowFactory("ImportJsonViewFactory").CreateWindow().Show();
        }
        private void ExportJson(object obj)
        {
            string json = JsonConvert.SerializeObject(Tours);
            string path= $"{HelperBase.GetExecutiveFullPath(ConfigurationManager.AppSettings["json_export_path"])}\\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.json";
            if (_fileService.ExportJson(path, json))
            {
                MessageBox.Show($"{Tours.Count} tours exported", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("There was an error exporting, please try again", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddTour(object obj)
        {
            GetWindowFactory("CreateTourViewFactory").CreateWindow().Show();
        }

        public void UpdateTours(string search="")
        {
            Tours = new ObservableCollection<Tour>(_databaseService.GetTours());
            foreach (Tour tour in Tours)
            {
                tour.LoadImage(_fileService);
                tour.LoadLogs(_databaseService);
            }

            Tours = new ObservableCollection<Tour>(Tours.Where(t => t.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
        }

        public void Update(ISubject subject)
        {
            UpdateTours();
        }
    }
}
