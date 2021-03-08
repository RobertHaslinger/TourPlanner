using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models.Geodata;
using TourPlanner.Services.Prediction;

namespace TourPlanner.ViewModels
{
    public class CreateTourViewModel : BaseViewModel
    {
        #region Fields

        private IPredictionService _predictionService;


        #endregion

        public CreateTourViewModel()
        {
            _predictionService = GetService<IPredictionService>();

            Predictions = new ObservableCollection<Location>();
        }
        
        #region Properties

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

        #endregion

        #region Methods

        protected override void OnPropertyChanged(string propertyName = null)
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
            if (!string.IsNullOrWhiteSpace(EndLocation) && IsEndLabelFocused)
            {
                IsEndPredictionListVisible = true;
                Predictions = new ObservableCollection<Location>(await _predictionService.FetchPredictions(EndLocation));
            }
            else
            {
                IsEndPredictionListVisible = false;
                Predictions.Clear();
            }
        }

        private async Task CheckStartLocationPredictions()
        {
            if (!string.IsNullOrWhiteSpace(EndLocation) && IsEndLabelFocused)
            {
                IsStartPredictionListVisible = true;
                Predictions = new ObservableCollection<Location>(await _predictionService.FetchPredictions(StartLocation));
            }
            else
            {
                IsStartPredictionListVisible = false;
                Predictions.Clear();
            }
        }

        #endregion
    }
}
