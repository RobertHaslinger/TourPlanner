using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Npgsql;
using NpgsqlTypes;
using TourPlanner.Enums;
using TourPlanner.Helper;
using TourPlanner.Models;

namespace TourPlanner.Services.Database
{
    public class PostgreSQLDatabaseService : IDatabaseService
    {
        private string _connectionString;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PostgreSQLDatabaseService(string connectionString)
        {
            _connectionString = connectionString;
            log4net.Config.XmlConfigurator.Configure();
        }

        public bool AddTour(Tour tour, out string tourImagePath)
        {
            int tourId;
            try
            {
                using NpgsqlConnection con= new NpgsqlConnection(_connectionString);
                con.Open();
                string statement =
                    "INSERT INTO  \"dev\".\"Tours\"(\"Name\", \"Description\", \"StartLocation\", \"EndLocation\", \"Distance\", \"Fuel\", \"RouteTime\", " +
                    "\"HasTollRoad\", \"HasFerry\", \"HasSeasonalClosure\", \"HasHighway\", \"HasUnpaved\", \"HasCountryCross\") " +
                    "VALUES(@name, @description, @startLocation, @endLocation, @distance, @fuel, @routeTime, @hasTollRoad, @hasFerry, " +
                    "@hasSeasonalClosure, @hasHighway, @hasUnpaved, @hasCountryCross) RETURNING \"Id\"";

                using (NpgsqlCommand cmd = new NpgsqlCommand(statement, con))
                {
                    cmd.Parameters.Add("name", NpgsqlDbType.Varchar).Value = tour.Name;
                    cmd.Parameters.Add("description", NpgsqlDbType.Varchar).Value = tour.Description;
                    cmd.Parameters.Add("startLocation", NpgsqlDbType.Varchar).Value = tour.StartLocation;
                    cmd.Parameters.Add("endLocation", NpgsqlDbType.Varchar).Value = tour.EndLocation;
                    cmd.Parameters.Add("distance", NpgsqlDbType.Double).Value = tour.Distance;
                    cmd.Parameters.Add("fuel", NpgsqlDbType.Double).Value = tour.FuelUsed;
                    cmd.Parameters.Add("routeTime", NpgsqlDbType.Varchar).Value = tour.EstimatedFormattedRouteTime ?? "unclear";
                    cmd.Parameters.Add("hasTollRoad", NpgsqlDbType.Boolean).Value = tour.HasTollRoad;
                    cmd.Parameters.Add("hasFerry", NpgsqlDbType.Boolean).Value = tour.HasFerry;
                    cmd.Parameters.Add("hasSeasonalClosure", NpgsqlDbType.Boolean).Value = tour.HasSeasonalClosure;
                    cmd.Parameters.Add("hasHighway", NpgsqlDbType.Boolean).Value = tour.HasHighway;
                    cmd.Parameters.Add("hasUnpaved", NpgsqlDbType.Boolean).Value = tour.HasUnpaved;
                    cmd.Parameters.Add("hasCountryCross", NpgsqlDbType.Boolean).Value = tour.HasCountryCross;

                    cmd.Prepare();
                    tourId= (int) cmd.ExecuteScalar();
                }

                tourImagePath = $"{HelperBase.GetExecutiveFullPath(ConfigurationManager.AppSettings["tour_image_path"])}\\{tourId}.png";
                statement = "UPDATE \"dev\".\"Tours\" SET \"ImagePath\"=@imagePath " +
                            "WHERE \"Id\"=@id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(statement, con))
                {
                    cmd.Parameters.Add("id", NpgsqlDbType.Integer).Value = tourId;
                    cmd.Parameters.Add("imagePath", NpgsqlDbType.Varchar).Value = tourImagePath;

                    cmd.Prepare();
                    _log.Info("Tour added");
                    return cmd.ExecuteNonQuery() == 1;
                }

            }
            catch (Exception e)
            {
                _log.Error("An Exception occurred while trying to add a new Tour", e);
                tourImagePath = "";
                return false;
            }
        }

        public bool AddTourLog(TourLog log)
        {
            try
            {
                using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
                con.Open();
                string statement =
                    "INSERT INTO  \"dev\".\"Logs\"(\"Name\", \"Report\", \"Distance\", \"TotalTime\", \"Rating\", \"AverageSpeed\", \"Vehicle\", " +
                    "\"EnergyUnitUsed\", \"TourId\") " +
                    "VALUES(@name, @report, @distance, @totalTime, @rating, @averageSpeed, @vehicle, @energyUnitUsed, @tourId) RETURNING \"Id\"";

                using (NpgsqlCommand cmd = new NpgsqlCommand(statement, con))
                {
                    cmd.Parameters.Add("name", NpgsqlDbType.Varchar).Value = log.Name;
                    cmd.Parameters.Add("report", NpgsqlDbType.Varchar).Value = log.Report;
                    cmd.Parameters.Add("distance", NpgsqlDbType.Double).Value = log.Distance;
                    cmd.Parameters.Add("totalTime", NpgsqlDbType.Varchar).Value = log.TotalTime;
                    cmd.Parameters.Add("rating", NpgsqlDbType.Varchar).Value = Enum.GetName(typeof(Rating), log.Rating);
                    cmd.Parameters.Add("averageSpeed", NpgsqlDbType.Double).Value = log.AverageSpeed;
                    cmd.Parameters.Add("vehicle", NpgsqlDbType.Varchar).Value = Enum.GetName(typeof(Vehicle), log.Vehicle);
                    cmd.Parameters.Add("energyUnitUsed", NpgsqlDbType.Double).Value = log.EnergyUnitUsed;
                    cmd.Parameters.Add("tourId", NpgsqlDbType.Integer).Value = log.TourId;

                    cmd.Prepare();
                    _log.Info("Tour log added");
                    return 1 == cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                _log.Error("An Exception occurred while trying to add a new Tour Log", e);
                return false;
            }
        }

        public List<TourLog> GetTourLogs(int tourId)
        {
            List<TourLog> logs = new List<TourLog>();
            try
            {
                using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
                con.Open();
                string statement = "SELECT * FROM \"dev\".\"Logs\" WHERE \"TourId\"=@tourId";

                using (NpgsqlCommand cmd = new NpgsqlCommand(statement, con))
                {
                    cmd.Parameters.Add("tourId", NpgsqlDbType.Integer).Value = tourId;
                    using NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        logs.Add(new TourLog(reader));
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                _log.Error("An Exception occurred while trying to fetch all tour logs", e);
            }

            return logs;
        }

        public void DeleteTour(int tourId)
        {
            try
            {
                using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
                con.Open();
                string statement = "DELETE FROM \"dev\".\"Tours\" WHERE \"Id\"=@id";

                using (NpgsqlCommand cmd = new NpgsqlCommand(statement, con))
                {
                    cmd.Parameters.Add("id", NpgsqlDbType.Integer).Value = tourId;
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    _log.Info("Tour deleted");
                }
            }
            catch (Exception e)
            {
                _log.Error($"An Exception occurred while trying to delete Tour (id:{tourId})", e);
            }
        }

        public bool EditTour(Tour editedTour, out string tourImagePath)
        {
            try
            {
                int tourId;
                using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
                con.Open();
                string statement =
                    "UPDATE \"dev\".\"Tours\" SET \"Name\"=@name, \"Description\"=@description, \"StartLocation\"=@startLocation, \"EndLocation\"=@endLocation, " +
                    "\"Distance\"=@distance, \"Fuel\"=@fuel, \"RouteTime\"=@routeTime, " +
                    "\"HasTollRoad\"=@hasTollRoad, \"HasFerry\"=@hasFerry, \"HasSeasonalClosure\"=@hasSeasonalClosure, \"HasHighway\"=@hasHighway, \"HasUnpaved\"=@hasUnpaved, \"HasCountryCross\"=@hasCountryCross " +
                    "WHERE \"Id\"=@id RETURNING \"Id\"";

                using (NpgsqlCommand cmd = new NpgsqlCommand(statement, con))
                {
                    cmd.Parameters.Add("id", NpgsqlDbType.Integer).Value = editedTour.Id;
                    cmd.Parameters.Add("name", NpgsqlDbType.Varchar).Value = editedTour.Name;
                    cmd.Parameters.Add("description", NpgsqlDbType.Varchar).Value = editedTour.Description;
                    cmd.Parameters.Add("startLocation", NpgsqlDbType.Varchar).Value = editedTour.StartLocation;
                    cmd.Parameters.Add("endLocation", NpgsqlDbType.Varchar).Value = editedTour.EndLocation;
                    cmd.Parameters.Add("distance", NpgsqlDbType.Double).Value = editedTour.Distance;
                    cmd.Parameters.Add("fuel", NpgsqlDbType.Double).Value = editedTour.FuelUsed;
                    cmd.Parameters.Add("routeTime", NpgsqlDbType.Varchar).Value = editedTour.EstimatedFormattedRouteTime ?? "unclear";
                    cmd.Parameters.Add("hasTollRoad", NpgsqlDbType.Boolean).Value = editedTour.HasTollRoad;
                    cmd.Parameters.Add("hasFerry", NpgsqlDbType.Boolean).Value = editedTour.HasFerry;
                    cmd.Parameters.Add("hasSeasonalClosure", NpgsqlDbType.Boolean).Value = editedTour.HasSeasonalClosure;
                    cmd.Parameters.Add("hasHighway", NpgsqlDbType.Boolean).Value = editedTour.HasHighway;
                    cmd.Parameters.Add("hasUnpaved", NpgsqlDbType.Boolean).Value = editedTour.HasUnpaved;
                    cmd.Parameters.Add("hasCountryCross", NpgsqlDbType.Boolean).Value = editedTour.HasCountryCross;

                    cmd.Prepare();
                    tourId = (int)cmd.ExecuteScalar();
                }

                tourImagePath = $"{HelperBase.GetExecutiveFullPath(ConfigurationManager.AppSettings["tour_image_path"])}\\{tourId}.png";
                statement = "UPDATE \"dev\".\"Tours\" SET \"ImagePath\"=@imagePath " +
                            "WHERE \"Id\"=@id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(statement, con))
                {
                    cmd.Parameters.Add("id", NpgsqlDbType.Integer).Value = tourId;
                    cmd.Parameters.Add("imagePath", NpgsqlDbType.Varchar).Value = tourImagePath;

                    cmd.Prepare();
                    _log.Info("Tour edited");
                    return cmd.ExecuteNonQuery() == 1;
                }

            }
            catch (Exception e)
            {
                _log.Error($"An Exception occurred while trying to edit a Tour", e);
                tourImagePath = "";
                return false;
            }
        }

        public List<Tour> GetTours()
        {
            List<Tour> tours= new List<Tour>();
            try
            {
                using NpgsqlConnection con = new NpgsqlConnection(_connectionString);
                con.Open();
                string statement = "SELECT * FROM \"dev\".\"Tours\"";

                using NpgsqlCommand cmd= new NpgsqlCommand(statement, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tours.Add(new Tour(reader));
                }
                reader.Close();

            }
            catch (Exception e)
            {
                _log.Error("An Exception occurred while trying to fetch all tours", e);
            }

            return tours;
        }
    }
}
