using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Npgsql;
using TourPlanner.Helper;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.Models
{
    public class Tour : NotifyPropertyChangedBase
    {
        public string ImagePath { get; }
        #region Properties

        private bool _hasTollRoad;

        public bool HasTollRoad
        {
            get { return _hasTollRoad; }
            set { _hasTollRoad = value; OnPropertyChanged(); }
        }

        private bool _hasFerry;

        public bool HasFerry
        {
            get { return _hasFerry; }
            set { _hasFerry = value; OnPropertyChanged(); }
        }

        private bool _hasSeasonalClosure;

        public bool HasSeasonalClosure
        {
            get { return _hasSeasonalClosure; }
            set { _hasSeasonalClosure = value; OnPropertyChanged(); }
        }

        private bool _hasHighway;

        public bool HasHighway
        {
            get { return _hasHighway; }
            set { _hasHighway = value; OnPropertyChanged(); }
        }

        private bool _hasUnpaved;

        public bool HasUnpaved
        {
            get { return _hasUnpaved; }
            set { _hasUnpaved = value; OnPropertyChanged(); }
        }

        private bool _hasCountryCross;

        public bool HasCountryCross
        {
            get { return _hasCountryCross; }
            set { _hasCountryCross = value; OnPropertyChanged(); }
        }

        private string _estimatedFormattedRouteTime;

        public string EstimatedFormattedRouteTime
        {
            get { return _estimatedFormattedRouteTime; }
            set { _estimatedFormattedRouteTime = value; OnPropertyChanged(); }
        }

        private double _distance;

        public double Distance
        {
            get { return _distance; }
            set { _distance = value; OnPropertyChanged(); }
        }

        private double _fuelUsed;

        public double FuelUsed
        {
            get { return _fuelUsed; }
            set { _fuelUsed = value; OnPropertyChanged(); }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged();}
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged();}
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged();}
        }

        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged();}
        }

        private string _startLocation;

        public string StartLocation
        {
            get { return _startLocation; }
            set { _startLocation = value; OnPropertyChanged();}
        }

        private string _endLocation;

        public string EndLocation
        {
            get { return _endLocation; }
            set { _endLocation = value; OnPropertyChanged(); }
        }
        #endregion

        public Tour()
        {
            
        }

        public Tour(NpgsqlDataReader reader)
        {
            Id = (int)reader["Id"];
            Name = reader["Name"].ToString();
            Description = reader["Description"].ToString();
            StartLocation = reader["StartLocation"].ToString();
            EndLocation = reader["EndLocation"].ToString();
            ImagePath = reader["ImagePath"].ToString();
            HasTollRoad = (bool)reader["HasTollRoad"];
            HasFerry = (bool)reader["HasFerry"];
            HasSeasonalClosure = (bool)reader["HasSeasonalClosure"];
            HasHighway = (bool)reader["HasHighway"];
            HasUnpaved = (bool)reader["HasUnpaved"];
            HasCountryCross = (bool)reader["HasCountryCross"];
            EstimatedFormattedRouteTime = reader["RouteTime"].ToString();
            Distance = (double)reader["Distance"];
            FuelUsed = (double) reader["Fuel"];
        }

        public bool HasSpecialties()
        {
            return HasTollRoad || HasFerry || HasSeasonalClosure || HasHighway || HasUnpaved || HasCountryCross;
        }

        public void LoadImage(IFileService fileService)
        {
            Image = HelperBase.LoadImage(fileService.GetImageBytes(ImagePath));
        }
    }
}
