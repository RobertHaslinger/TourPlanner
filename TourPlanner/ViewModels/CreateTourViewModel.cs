using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TourPlanner.Helper;
using TourPlanner.Models.Geodata;
using TourPlanner.Services.Prediction;

namespace TourPlanner.ViewModels
{
    public class CreateTourViewModel : BaseViewModel
    {
        #region Fields

        private IPredictionService _predictionService => GetService<IPredictionService>();

        #endregion

        public CreateTourViewModel()
        {
            Predictions = new ObservableCollection<Location>();
        }

        #region Properties

        private string _tourName;

        public string TourName
        {
            get { return _tourName; }
            set { _tourName = value; OnPropertyChanged();}
        }

        private string _tourDescription;

        public string TourDescription
        {
            get { return _tourDescription; }
            set { _tourDescription = value; OnPropertyChanged();}
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
            set { _realStartLocation = value; OnPropertyChanged();}
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
            set { _isStartPredictionListLoading = value; OnPropertyChanged();}
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
            set { _isStartErrorDisplayed = value; OnPropertyChanged();}
        }

        private bool _isEndErrorDisplayed;

        public bool IsEndErrorDisplayed
        {
            get { return _isEndErrorDisplayed; }
            set { _isEndErrorDisplayed = value; OnPropertyChanged();}
        }

        #endregion

        #region Commands

        public ICommand ClearAllCommand => new RelayCommand(ClearAllInputs);
        public ICommand PreviewRouteCommand => new RelayCommand(PreviewRoute);

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
                CancellationTokenSource source= new CancellationTokenSource();
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

        private void PreviewRoute(object obj)
        {
            throw new NotImplementedException();
        }

        private void ClearAllInputs(object obj)
        {
            TourName = "";
            TourDescription = "";
            StartLocation = "";
            RealStartLocation = null;
            EndLocation = "";
            RealEndLocation = null;
            ClearPredictions();
        }

        #endregion
    }
}
