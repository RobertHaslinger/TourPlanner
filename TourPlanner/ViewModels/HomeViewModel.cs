using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Helper.Observer;
using TourPlanner.Models;
using TourPlanner.Services;
using TourPlanner.Services.Database;
using TourPlanner.Services.Greet;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels
{
    public class HomeViewModel : BaseViewModel, ISubject
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IFileService _fileService => GetService<IFileService>();
        private List<IObserver> _observers = new List<IObserver>();

        private string _greetMessage;

        public string GreetMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_greetMessage))
                {
                    LoadGreetMessage();
                }

                return _greetMessage;
            }
            set { _greetMessage = value; OnPropertyChanged(); }
        }

        private Tour _selectedTour;

        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set { _selectedTour = value; OnPropertyChanged();}
        }

        private ObservableCollection<TourLog> _selectedTourLogs;

        public ObservableCollection<TourLog> SelectedTourLogs
        {
            get { return _selectedTourLogs; }
            set { _selectedTourLogs = value; OnPropertyChanged();}
        }

        public ICommand SelectedTourChangedCommand => new RelayCommand(OnSelectedTourChangedCommandExecuted);
        public ICommand SelectedTourEditedCommand => new RelayCommand(OnSelectedTourEditedCommandExecuted);
        public ICommand SelectedTourCopiedCommand => new RelayCommand(OnSelectedTourCopiedCommandExecuted);
        public ICommand SelectedTourDeletedCommand => new RelayCommand(OnSelectedTourDeletedCommandExecuted);
        public ICommand SearchTextChangedCommand => new RelayCommand(OnSearchTextChangedCommandExecuted);

        private void OnSearchTextChangedCommandExecuted(object obj)
        {
            string search = obj.ToString();
            if (SelectedTour == null)
            {
                return;
            }


            SelectedTourLogs = new ObservableCollection<TourLog>(SelectedTour.Logs.Where(l => l.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)));
        }

        private void OnSelectedTourEditedCommandExecuted(object obj)
        {
            Tour tour = (Tour) obj;
            GetWindowFactory("EditTourViewFactory").CreateWindow(new Dictionary<string, object>() {{"tour", tour}}).Show();
        }


        private void OnSelectedTourDeletedCommandExecuted(object obj)
        {
            Tour tour = (Tour) obj;
            if (MessageBox.Show($"Delete {tour.Name}?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) ==
                MessageBoxResult.Yes)
            {
                try
                {
                    BaseObserverSingleton.GetInstance.TourObservers.ForEach(Attach);
                    _databaseService.DeleteTour(tour.Id);
                    _fileService.DeleteImage(tour.ImagePath);
                    MessageBox.Show("Tour deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Notify();
                    BaseObserverSingleton.GetInstance.TourObservers.ForEach(Detach);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    MessageBox.Show("There was an error deleting the tour, please try again", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private void OnSelectedTourCopiedCommandExecuted(object obj)
        {
            Tour tour = (Tour)obj;
            string imagePath;
            if (_databaseService.AddTour(tour, out imagePath) && _fileService.SaveImage(imagePath, tour.Image))
            {
                BaseObserverSingleton.GetInstance.TourObservers.ForEach(Attach);
                MessageBox.Show($"Tour \"{tour.Name}\" copied", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Notify();
                BaseObserverSingleton.GetInstance.TourObservers.ForEach(Detach);
            }
            else
            {
                MessageBox.Show("There was an error, please try again.", "Failed saving", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void OnSelectedTourChangedCommandExecuted(object obj)
        {
            SelectedTour = (Tour) obj;
            SelectedTourLogs = SelectedTour != null ? SelectedTour.Logs : new ObservableCollection<TourLog>();
        }

        private void LoadGreetMessage()
        {
            GreetMessage = GetService<IGreetService>().Greet();
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
