using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.DataAccessLayer.Models.Geodata;
using TourPlanner.DataAccessLayer.Models.Route;
using TourPlanner.Helper;
using TourPlanner.Helper.Observer;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.Direction;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Map;
using TourPlanner.Services.Prediction;

namespace TourPlanner.ViewModels
{
    public class EditTourViewModel : BaseViewModel, ISubject
    {
        #region Fields

        private IPredictionService _predictionService => GetService<IPredictionService>();
        private IMapService _mapService => GetService<IMapService>();
        private IDirectionService _directionService => GetService<IDirectionService>();
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IFileService _fileService => GetService<IFileService>();
        private List<IObserver> _observers = new List<IObserver>();
        private int _tourId;

        #endregion

        public EditTourViewModel()
        {
            Predictions = new ObservableCollection<Location>();
        }

        #region Properties

        private string _tourName;

        public string TourName
        {
            get { return _tourName; }
            set { _tourName = value; OnPropertyChanged(); }
        }

        private string _tourDescription;

        public string TourDescription
        {
            get { return _tourDescription; }
            set { _tourDescription = value; OnPropertyChanged(); }
        }



        private string _start;

        public string StartLocation
        {
            get => _start;
            set
            {
                _start = value;
                OnPropertyChanged();

            }
        }

        private Location _realStartLocation;

        public Location RealStartLocation
        {
            get { return _realStartLocation; }
            set { _realStartLocation = value; OnPropertyChanged(); }
        }


        private string _end;

        public string EndLocation
        {
            get => _end;
            set
            {
                _end = value;
                OnPropertyChanged();

            }
        }

        private Location _realEndLocation;

        public Location RealEndLocation
        {
            get { return _realEndLocation; }
            set { _realEndLocation = value; OnPropertyChanged(); }
        }

        private bool _isStartLabelFocused;

        public bool IsStartLabelFocused
        {
            get => _isStartLabelFocused;
            set
            {
                _isStartLabelFocused = value;
                OnPropertyChanged();
            }
        }

        private bool _isStartPredictionListVisible;

        public bool IsStartPredictionListVisible
        {
            get => _isStartPredictionListVisible;
            set
            {
                _isStartPredictionListVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isEndLabelFocused;

        public bool IsEndLabelFocused
        {
            get => _isEndLabelFocused;
            set
            {
                _isEndLabelFocused = value;
                OnPropertyChanged();
            }
        }

        private bool _isEndPredictionListVisible;

        public bool IsEndPredictionListVisible
        {
            get => _isEndPredictionListVisible;
            set
            {
                _isEndPredictionListVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Location> _predictions;

        public ObservableCollection<Location> Predictions
        {
            get => _predictions;
            set
            {
                _predictions = value;
                OnPropertyChanged();
            }
        }

        private Location _selectedStartPrediction;

        public Location SelectedStartPrediction
        {
            get => _selectedStartPrediction;
            set
            {
                _selectedStartPrediction = value;
                OnPropertyChanged();
                if (value == null) return;

                StartLocation = value.DisplayName;
                RealStartLocation = value;
                IsStartLabelFocused = false;
            }
        }

        private Location _selectedEndPrediction;

        public Location SelectedEndPrediction
        {
            get => _selectedEndPrediction;
            set
            {
                _selectedEndPrediction = value;
                OnPropertyChanged();
                if (value == null) return;

                EndLocation = value.DisplayName;
                RealEndLocation = value;
                IsEndLabelFocused = false;
            }
        }

        private bool _isStartPredictionListLoading;

        public bool IsStartPredictionListLoading
        {
            get { return _isStartPredictionListLoading; }
            set { _isStartPredictionListLoading = value; OnPropertyChanged(); }
        }

        private bool _isEndPredictionListLoading;

        public bool IsEndPredictionListLoading
        {
            get { return _isEndPredictionListLoading; }
            set { _isEndPredictionListLoading = value; OnPropertyChanged(); }
        }

        private int _startLoadingProgress;

        public int StartLoadingProgress
        {
            get { return _startLoadingProgress; }
            set { _startLoadingProgress = value; OnPropertyChanged(); }
        }

        private int _endLoadingProgress;

        public int EndLoadingProgress
        {
            get { return _endLoadingProgress; }
            set { _endLoadingProgress = value; OnPropertyChanged(); }
        }

        private bool _isStartErrorDisplayed;

        public bool IsStartErrorDisplayed
        {
            get { return _isStartErrorDisplayed; }
            set { _isStartErrorDisplayed = value; OnPropertyChanged(); }
        }

        private bool _isEndErrorDisplayed;

        public bool IsEndErrorDisplayed
        {
            get { return _isEndErrorDisplayed; }
            set { _isEndErrorDisplayed = value; OnPropertyChanged(); }
        }

        private BitmapImage _previewMap;

        public BitmapImage PreviewMap
        {
            get => _previewMap;
            set { _previewMap = value; OnPropertyChanged(); }
        }

        private Route _previewRoute;

        public Route PreviewRoute
        {
            get => _previewRoute;
            set
            {
                _previewRoute = value;
                OnPropertyChanged();
                IsSaveVisible = value != null;
            }
        }

        private bool _routeHasAnySpecialities;

        public bool RouteHasAnySpecialities
        {
            get { return _routeHasAnySpecialities; }
            set { _routeHasAnySpecialities = value; OnPropertyChanged(); }
        }

        private bool _isRouteInfoAvailable;

        public bool IsRouteInfoAvailable
        {
            get { return _isRouteInfoAvailable; }
            set { _isRouteInfoAvailable = value; OnPropertyChanged(); }
        }

        private bool _isSaveVisible;

        public bool IsSaveVisible
        {
            get { return _isSaveVisible; }
            set { _isSaveVisible = value; OnPropertyChanged(); }
        }


        #endregion

        #region Commands

        public ICommand CancelCommand => new RelayCommand(CancelCommandExecuted);
        public ICommand PreviewRouteCommand => new RelayCommand(async sender => await StartPreviewRoute(sender));
        public ICommand SaveTourCommand => new RelayCommand(SaveTourInDatabase);

        #endregion

        #region Methods

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(IsStartLabelFocused) || propertyName == nameof(StartLocation))
            {
                Task.Run(CheckStartLocationPredictions);
            }
            else if (propertyName == nameof(IsEndLabelFocused) || propertyName == nameof(EndLocation))
            {
                Task.Run(CheckEndLocationPredictions);
            }
        }

        private async Task CheckEndLocationPredictions()
        {
            if (!string.IsNullOrWhiteSpace(EndLocation) && EndLocation.Length > 1 && IsEndLabelFocused)
            {
                IsEndPredictionListVisible = true;
                IsEndPredictionListLoading = true;
                CancellationTokenSource source = new CancellationTokenSource();
                EndLoadingProgress = 0;
                RepeatActionAfter(() =>
                {
                    EndLoadingProgress += 3;
                }, TimeSpan.FromMilliseconds(10), source.Token);
                Predictions = new ObservableCollection<Location>(await _predictionService.FetchPredictions(EndLocation));
                source.Cancel();
                EndLoadingProgress = 100;
                //simulate 100% on progress bar
                await Task.Delay(100);
                IsEndPredictionListLoading = false;
            }
            else
            {
                IsEndPredictionListVisible = false;
                ClearPredictions();
                EndLoadingProgress = 0;
                IsEndPredictionListLoading = false;
            }

            IsEndErrorDisplayed = !string.IsNullOrWhiteSpace(EndLocation) && EndLocation.Length > 1 && RealEndLocation == null;
        }

        private async Task CheckStartLocationPredictions()
        {
            if (!string.IsNullOrWhiteSpace(StartLocation) && StartLocation.Length > 1 && IsStartLabelFocused)
            {
                IsStartPredictionListVisible = true;
                IsStartPredictionListLoading = true;
                CancellationTokenSource source = new CancellationTokenSource();
                StartLoadingProgress = 0;
                RepeatActionAfter(() =>
                {
                    StartLoadingProgress += 3;
                }, TimeSpan.FromMilliseconds(10), source.Token);
                Predictions = new ObservableCollection<Location>(await _predictionService.FetchPredictions(StartLocation));
                source.Cancel();
                StartLoadingProgress = 100;
                //simulate 100% on progress bar
                await Task.Delay(100);
                IsStartPredictionListLoading = false;
            }
            else
            {
                IsStartPredictionListVisible = false;
                ClearPredictions();
                StartLoadingProgress = 0;
                IsStartPredictionListLoading = false;
            }

            IsStartErrorDisplayed = !string.IsNullOrWhiteSpace(StartLocation) && StartLocation.Length > 1 && RealStartLocation == null;
        }

        private delegate void NoParamDelegate();

        /// <summary>
        /// This Method clears the predictions collection on the UI Thread
        /// </summary>
        private void ClearPredictions()
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                Predictions.Clear();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(new NoParamDelegate(ClearPredictions));
            }
        }

        private async Task StartPreviewRoute(object obj)
        {
            if (RealStartLocation == null || RealEndLocation == null)
            {
                MessageBox.Show("You need to fill in both locations!", "Form error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            PreviewMap = await _mapService.GetMapWithLocations(RealStartLocation, RealEndLocation);
            PreviewRoute = await _directionService.GetSimpleRoute(RealStartLocation, RealEndLocation);
            RouteHasAnySpecialities = PreviewRoute != null && PreviewRoute.HasSpecialties();
            IsRouteInfoAvailable = PreviewRoute != null;
        }

        private void CancelCommandExecuted(object obj)
        {
            if (MessageBox.Show("Are you sure that you want to cancel editing? Progress will not be saved", "Cancel",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ((Window)obj).Close();
            }
        }



        private void SaveTourInDatabase(object obj)
        {
            if (RealStartLocation == null || RealEndLocation == null || PreviewRoute == null || PreviewMap == null || TourName == null)
            {
                MessageBox.Show("You need set a name, fill in both locations and look at the preview before saving", "Form error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            Tour tour = new Tour()
            {
                Id= _tourId,
                Name = TourName,
                Description = TourDescription,
                StartLocation = StartLocation,
                EndLocation = EndLocation,
                Image = PreviewMap,
                Distance = PreviewRoute.Distance,
                FuelUsed = PreviewRoute.FuelUsed,
                EstimatedFormattedRouteTime = PreviewRoute.EstimatedFormattedRouteTime,
                HasFerry = PreviewRoute.HasFerry,
                HasTollRoad = PreviewRoute.HasTollRoad,
                HasSeasonalClosure = PreviewRoute.HasSeasonalClosure,
                HasHighway = PreviewRoute.HasHighway,
                HasUnpaved = PreviewRoute.HasUnpaved,
                HasCountryCross = PreviewRoute.HasCountryCross
            };
            string imagePath;
            if (_databaseService.EditTour(tour, out imagePath) && _fileService.SaveImage(imagePath, tour.Image))
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

        public void InitTour(Tour tour)
        {
            TourName = tour.Name;
            TourDescription = tour.Description;
            _tourId = tour.Id;
        }
        #endregion
    }
}
