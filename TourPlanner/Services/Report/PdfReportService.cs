using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using TourPlanner.Enums;
using TourPlanner.Helper;
using TourPlanner.Models;
using Paragraph = iText.Layout.Element.Paragraph;

namespace TourPlanner.Services.Report
{
    public class PdfReportService : ReportServiceBase, IReportService
    {
        public PdfReportService()
        {
            DirPath = $"{ConfigurationManager.AppSettings["download_dir_path"]}\\Reports\\";
        }
        public void GenerateReport(Tour tour)
        {
            GuaranteeFileAccess();
            string filename =
                $"{DirPath}{Regex.Replace(tour.Name, @"\s+", "")}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}_Report.pdf";
            Document document = new Document(new PdfDocument(new PdfWriter(filename)));

            document
                .Add(new Paragraph($"Report for Tour: {tour.Name}")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetMarginBottom(20))
                .Add(new Paragraph($"Description: {tour.Description}")
                    .SetHorizontalAlignment(HorizontalAlignment.LEFT)
                    .SetMarginBottom(10))
                .Add(new Paragraph($"Start: {tour.StartLocation}")
                    .SetHorizontalAlignment(HorizontalAlignment.LEFT)
                    .SetMarginBottom(5))
                .Add(new Paragraph($"End: {tour.EndLocation}")
                    .SetHorizontalAlignment(HorizontalAlignment.LEFT)
                    .SetMarginBottom(20))
                .Add(new Image(ImageDataFactory.CreatePng(HelperBase.ImageToByteArray(tour.Image)))
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                    .SetMarginBottom(20))
                .Add(new Paragraph($"Specialties: {tour.GetSpecialtiesString()}")
                    .SetHorizontalAlignment(HorizontalAlignment.LEFT)
                    .SetMarginBottom(10))
                .Add(new Paragraph("Logs:")
                    .SetMarginBottom(3));

            int counter = 1;
            Paragraph row;
            foreach (TourLog tourLog in tour.Logs)
            {
                document.Add(new Paragraph(
                    $"{counter}. {tourLog.Name} ({Enum.GetName(typeof(Vehicle), tourLog.Vehicle)}, Rating: {Enum.GetName(typeof(Rating), tourLog.Rating)}): " +
                    $"{tourLog.Distance}km in {tourLog.TotalTime} with an average speed of {tourLog.AverageSpeed}km/h."));
                counter++;
            }

            document.Close();
        }

        public void GenerateSummaryReport(string tourName, List<TourLog> logs)
        {
            GuaranteeFileAccess();
            string filename =
                $"{DirPath}{Regex.Replace(tourName, @"\s+", "")}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}_StatisticReport.pdf";
            Document document = new Document(new PdfDocument(new PdfWriter(filename)));
            Dictionary<string, double> sums = new Dictionary<string, double>
            {
                ["distance"] = 0, ["time"] = 0, ["count"] = 0
            };
            logs.ForEach((log) =>
            {
                sums["count"]++;
                sums["distance"] += log.Distance;
                TimeSpan time= TimeSpan.Parse(log.TotalTime);
                sums["time"] += time.Ticks;
            });

            document
                .Add(new Paragraph($"Statistic Report for Tour: {tourName}")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetMarginBottom(20))
                .Add(new Paragraph($"Logs: {logs.Count}\nTotal Distance: {sums["distance"]} km\nTotal Time: {new TimeSpan((long)sums["time"]):g}")
                    .SetHorizontalAlignment(HorizontalAlignment.LEFT)
                    .SetMarginBottom(10))
                .Add(new Paragraph("Logs:")
                    .SetMarginBottom(3));

            int counter = 1;
            Paragraph row;
            foreach (TourLog tourLog in logs)
            {
                document.Add(new Paragraph(
                    $"{counter}. {tourLog.Name} ({Enum.GetName(typeof(Vehicle), tourLog.Vehicle)}, Rating: {Enum.GetName(typeof(Rating), tourLog.Rating)}): " +
                    $"{tourLog.Distance}km in {tourLog.TotalTime} with an average speed of {tourLog.AverageSpeed}km/h."));
                counter++;
            }

            document.Close();
        }
    }
}
