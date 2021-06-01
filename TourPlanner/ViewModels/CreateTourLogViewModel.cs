using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Enums;
using TourPlanner.Helper;
using TourPlanner.Helper.Observer;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels
{
    public class CreateTourLogViewModel : BaseViewModel, ISubject
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private List<IObserver> _observers = new List<IObserver>();

        #region Properties

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _report;

        public string Report
        {
            get { return _report; }
            set { _report = value; OnPropertyChanged(); }
        }

        private string _totalTime;

        public string TotalTime
        {
            get { return _totalTime; }
            set { _totalTime = value; OnPropertyChanged(); }
        }

        private Rating _rating;

        public Rating Rating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(); }
        }

        private double? _distance;

        public double? Distance
        {
            get { return _distance; }
            set { _distance = value; OnPropertyChanged(); }
        }

        private double? _energyUnitUsed;

        public double? EnergyUnitUsed
        {
            get { return _energyUnitUsed; }
            set { _energyUnitUsed = value; OnPropertyChanged(); }
        }

        private Vehicle _vehicle;

        public Vehicle Vehicle
        {
            get { return _vehicle; }
            set { _vehicle = value; OnPropertyChanged();
                UpdateEnergyUnit(value);
            }
        }

        private void UpdateEnergyUnit(Vehicle value)
        {
            switch (value)
            {
                case Vehicle.Car:
                    EnergyUnit = "Fuel used";
                    break;
                default:
                    EnergyUnit = "Burnt calories";
                    break;
            }
        }

        private string _energyUnit = "Burnt calories";

        public string EnergyUnit
        {
            get { return _energyUnit; }
            set { _energyUnit = value; OnPropertyChanged();}
        }


        private double? _averageSpeed;

        public double? AverageSpeed
        {
            get { return _averageSpeed; }
            set { _averageSpeed = value; OnPropertyChanged(); }
        }

        public int TourId { get; set; }

        public IEnumerable<Rating> PossibleRatings =>
            Enum.GetValues(typeof(Rating))
                .Cast<Rating>();

        public IEnumerable<Vehicle> PossibleVehicles =>
            Enum.GetValues(typeof(Vehicle))
                .Cast<Vehicle>();
        #endregion

        public ICommand SaveCommand => new RelayCommand(SaveLog);

        private void SaveLog(object obj)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Report))
            {
                MessageBox.Show("You need set a name and fill in a report before saving", "Form error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (TourId == -1)
            {
                MessageBox.Show("The tour for the log no longer exists", "Form error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            TourLog log = new TourLog()
            {
                Name = Name,
                Report = Report,
                Vehicle = Vehicle,
                Rating = Rating,
                AverageSpeed = AverageSpeed ?? 0,
                Distance = Distance ?? 0,
                EnergyUnitUsed = EnergyUnitUsed ?? 0,
                TotalTime = TotalTime,
                TourId = TourId
            };
            if (_databaseService.AddTourLog(log))
            {
                ((Window)obj).Close();
                Notify();
            }
            else
            {
                MessageBox.Show("There was an error, please try again.", "Failed saving", MessageBoxButton.OK,
                    MessageBoxImage.Error);
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
