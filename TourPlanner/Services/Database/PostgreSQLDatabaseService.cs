using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using TourPlanner.Helper;
using TourPlanner.Models;

namespace TourPlanner.Services.Database
{
    public class PostgreSQLDatabaseService : IDatabaseService
    {
        private string _connectionString;

        public PostgreSQLDatabaseService(string connectionString)
        {
            _connectionString = connectionString;
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
                    cmd.Parameters.Add("routeTime", NpgsqlDbType.Varchar).Value = tour.EstimatedFormattedRouteTime;
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
                    return cmd.ExecuteNonQuery() == 1;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                tourImagePath = "";
                return false;
            }
        }

        public void DeleteTour(int tourId)
        {
            throw new NotImplementedException();
        }

        public void EditTour(Tour editedTour)
        {
            throw new NotImplementedException();
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
                Console.WriteLine(e);
            }

            return tours;
        }
    }
}
