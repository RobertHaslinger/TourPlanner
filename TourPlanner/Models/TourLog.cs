using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TourPlanner.Enums;
using TourPlanner.Helper;

namespace TourPlanner.Models
{
    public class TourLog : NotifyPropertyChangedBase
    {
        public int TourId { get; set; }
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

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

        private double _distance;

        public double Distance
        {
            get { return _distance; }
            set { _distance = value; OnPropertyChanged(); }
        }

        private double _energyUnitUsed;

        public double EnergyUnitUsed
        {
            get { return _energyUnitUsed; }
            set { _energyUnitUsed = value; OnPropertyChanged(); }
        }

        private Vehicle _vehicle;

        public Vehicle Vehicle
        {
            get { return _vehicle; }
            set { _vehicle = value; OnPropertyChanged(); }
        }

        private double _averageSpeed;

        public double AverageSpeed
        {
            get { return _averageSpeed; }
            set { _averageSpeed = value; OnPropertyChanged(); }
        }

        public TourLog()
        {

        }

        public TourLog(NpgsqlDataReader reader)
        {
            Id = (int)reader["Id"];
            TourId = (int)reader["TourId"];
            Name = reader["Name"].ToString();
            Report = reader["Report"].ToString();
            TotalTime = reader["TotalTime"].ToString();
            Vehicle = (Vehicle)Enum.Parse(typeof(Vehicle),reader["Vehicle"].ToString());
            Rating = (Rating)Enum.Parse(typeof(Rating), reader["Rating"].ToString());
            EnergyUnitUsed = (double)reader["EnergyUnitUsed"];
            Distance = (double)reader["Distance"];
            AverageSpeed = (double)reader["AverageSpeed"];
        }
    }
}
