using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Moq;
using NUnit.Framework;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.Direction;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Map;
using TourPlanner.ViewModels;

namespace TourPlanner.Test
{
    [TestFixture]
    public class ImportJsonViewModelTests
    {
        private ImportJsonViewModel _vm;
        private Mock<IDatabaseService> _databaseMock;
        private Mock<IFileService> _fileMock;
        private Mock<IMapService> _mapMock;
        private string _validJson;
        private string _invalidJson;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _validJson =
                "[{\"Name\":\"Austria - Netherlands\",\"Description\":\"Titel sagts eh\",\"StartLocation\":\"Austria\",\"EndLocation\":\"The Netherlands\",\"Distance\":624.7399,\"EstimatedFormattedRouteTime\":\"09:56:06\",\"Id\":39,\"Logs\":[{\"Name\":\"Roberts Roadtrip\",\"Report\":\"Ein toller Roadtrip\",\"Vehicle\":3,\"Distance\":37.0,\"EnergyUnitUsed\":7.0,\"TotalTime\":\"05:12:34\",\"AverageSpeed\":87.0,\"Rating\":3,\"TourId\":39,\"Id\":1},{\"Name\":\"Test log\",\"Report\":\"test\",\"Vehicle\":0,\"Distance\":10.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"12:34:56\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":39,\"Id\":2},{\"Name\":\"2\",\"Report\":\"2\",\"Vehicle\":0,\"Distance\":0.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"3\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":39,\"Id\":3},{\"Name\":\"4\",\"Report\":\"asdas\",\"Vehicle\":3,\"Distance\":0.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"11:11:11\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":39,\"Id\":6}],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\39.png\",\"HasTollRoad\":true,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":true,\"HasUnpaved\":false,\"HasCountryCross\":true,\"FuelUsed\":29.74},{\"Name\":\"Tolle neue Tour\",\"Description\":\"Lol\",\"StartLocation\":\"Seattle, King County, WA, US\",\"EndLocation\":\"United Kingdom\",\"Distance\":0.0,\"EstimatedFormattedRouteTime\":\"unclear\",\"Id\":41,\"Logs\":[],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\41.png\",\"HasTollRoad\":false,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":false,\"HasUnpaved\":false,\"HasCountryCross\":false,\"FuelUsed\":0.0},{\"Name\":\"New York - Florida\",\"Description\":\"Chillig\",\"StartLocation\":\"New York, New York County, NY, US\",\"EndLocation\":\"Florence, Florence County, SC, US\",\"Distance\":0.0,\"EstimatedFormattedRouteTime\":\"unclear\",\"Id\":40,\"Logs\":[{\"Name\":\"234\",\"Report\":\"234\",\"Vehicle\":0,\"Distance\":0.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"234\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":40,\"Id\":4},{\"Name\":\"asd\",\"Report\":\"asd\",\"Vehicle\":0,\"Distance\":0.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"asd\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":40,\"Id\":5}],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\40.png\",\"HasTollRoad\":false,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":false,\"HasUnpaved\":false,\"HasCountryCross\":false,\"FuelUsed\":0.0},{\"Name\":\"Dallas - Texas\",\"Description\":\".\",\"StartLocation\":\"Dallas-Fort Worth International Airport (DFW), 3200 E Airfield Dr, Dallas, TX\",\"EndLocation\":\"Texas, US\",\"Distance\":202.531,\"EstimatedFormattedRouteTime\":\"03:11:57\",\"Id\":42,\"Logs\":[{\"Name\":\"Roadtrip\",\"Report\":\"Ein toller Roadtrip zu einer tollen Route\",\"Vehicle\":3,\"Distance\":125.0,\"EnergyUnitUsed\":12.0,\"TotalTime\":\"03:12:55\",\"AverageSpeed\":70.0,\"Rating\":2,\"TourId\":42,\"Id\":7}],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\42.png\",\"HasTollRoad\":true,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":true,\"HasUnpaved\":false,\"HasCountryCross\":false,\"FuelUsed\":9.73},{\"Name\":\"Tour test\",\"Description\":\"Description\",\"StartLocation\":\"Dallas, Dallas County, TX, US\",\"EndLocation\":\"Texas, US\",\"Distance\":213.695,\"EstimatedFormattedRouteTime\":\"03:24:37\",\"Id\":43,\"Logs\":[{\"Name\":\"1. Log\",\"Report\":\"Mein Report\",\"Vehicle\":3,\"Distance\":143.0,\"EnergyUnitUsed\":12.0,\"TotalTime\":\"12:55:32\",\"AverageSpeed\":78.0,\"Rating\":4,\"TourId\":43,\"Id\":8}],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\43.png\",\"HasTollRoad\":false,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":true,\"HasUnpaved\":false,\"HasCountryCross\":false,\"FuelUsed\":10.12}]";
            _invalidJson =
                "[{\"Name\":\"\",\"Description\":\"Titel sagts eh\",\"StartLocation\":\"Austria\",\"EndLocation\":\"The Netherlands\",\"Distance\":624.7399,\"EstimatedFormattedRouteTime\":\"09:56:06\",\"Id\":39,\"Logs\":[{\"Name\":\"\",\"Report\":\"Ein toller Roadtrip\",\"Vehicle\":3,\"Distance\":37.0,\"EnergyUnitUsed\":7.0,\"TotalTime\":\"05:12:34\",\"AverageSpeed\":87.0,\"Rating\":3,\"TourId\":39,\"Id\":1},{\"Name\":\"Test log\",\"Report\":\"test\",\"Vehicle\":0,\"Distance\":10.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"12:34:56\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":39,\"Id\":2},{\"Name\":\"2\",\"Report\":\"2\",\"Vehicle\":0,\"Distance\":0.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"3\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":39,\"Id\":3},{\"Name\":\"4\",\"Report\":\"asdas\",\"Vehicle\":3,\"Distance\":0.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"11:11:11\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":39,\"Id\":6}],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\39.png\",\"HasTollRoad\":true,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":true,\"HasUnpaved\":false,\"HasCountryCross\":true,\"FuelUsed\":29.74},{\"Name\":\"Tolle neue Tour\",\"Description\":\"Lol\",\"StartLocation\":\"Seattle, King County, WA, US\",\"EndLocation\":\"United Kingdom\",\"Distance\":0.0,\"EstimatedFormattedRouteTime\":\"unclear\",\"Id\":41,\"Logs\":[],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\41.png\",\"HasTollRoad\":false,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":false,\"HasUnpaved\":false,\"HasCountryCross\":false,\"FuelUsed\":0.0},{\"Name\":\"New York - Florida\",\"Description\":\"Chillig\",\"StartLocation\":\"New York, New York County, NY, US\",\"EndLocation\":\"Florence, Florence County, SC, US\",\"Distance\":0.0,\"EstimatedFormattedRouteTime\":\"unclear\",\"Id\":40,\"Logs\":[{\"Name\":\"234\",\"Report\":\"234\",\"Vehicle\":0,\"Distance\":0.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"234\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":40,\"Id\":4},{\"Name\":\"asd\",\"Report\":\"asd\",\"Vehicle\":0,\"Distance\":0.0,\"EnergyUnitUsed\":0.0,\"TotalTime\":\"asd\",\"AverageSpeed\":0.0,\"Rating\":0,\"TourId\":40,\"Id\":5}],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\40.png\",\"HasTollRoad\":false,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":false,\"HasUnpaved\":false,\"HasCountryCross\":false,\"FuelUsed\":0.0},{\"Name\":\"Dallas - Texas\",\"Description\":\".\",\"StartLocation\":\"Dallas-Fort Worth International Airport (DFW), 3200 E Airfield Dr, Dallas, TX\",\"EndLocation\":\"Texas, US\",\"Distance\":202.531,\"EstimatedFormattedRouteTime\":\"03:11:57\",\"Id\":42,\"Logs\":[{\"Name\":\"Roadtrip\",\"Report\":\"Ein toller Roadtrip zu einer tollen Route\",\"Vehicle\":3,\"Distance\":125.0,\"EnergyUnitUsed\":12.0,\"TotalTime\":\"03:12:55\",\"AverageSpeed\":70.0,\"Rating\":2,\"TourId\":42,\"Id\":7}],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\42.png\",\"HasTollRoad\":true,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":true,\"HasUnpaved\":false,\"HasCountryCross\":false,\"FuelUsed\":9.73},{\"Name\":\"Tour test\",\"Description\":\"Description\",\"StartLocation\":\"Dallas, Dallas County, TX, US\",\"EndLocation\":\"Texas, US\",\"Distance\":213.695,\"EstimatedFormattedRouteTime\":\"03:24:37\",\"Id\":43,\"Logs\":[{\"Name\":\"1. Log\",\"Report\":\"Mein Report\",\"Vehicle\":3,\"Distance\":143.0,\"EnergyUnitUsed\":12.0,\"TotalTime\":\"12:55:32\",\"AverageSpeed\":78.0,\"Rating\":4,\"TourId\":43,\"Id\":8}],\"ImagePath\":\"C:\\\\Users\\\\Robert\\\\source\\\\repos\\\\TourPlanner\\\\TourPlanner\\\\bin\\\\Debug\\\\net5.0-windows\\\\tours\\\\43.png\",\"HasTollRoad\":false,\"HasFerry\":false,\"HasSeasonalClosure\":false,\"HasHighway\":true,\"HasUnpaved\":false,\"HasCountryCross\":false,\"FuelUsed\":10.12}]";
        }

        [SetUp]
        public void SetUp()
        {
            _vm = new ImportJsonViewModel();
            _databaseMock = new Mock<IDatabaseService>();
            _fileMock = new Mock<IFileService>();
            _mapMock = new Mock<IMapService>();

            _vm.ServiceLocator.RegisterService(_databaseMock.Object);
            _vm.ServiceLocator.RegisterService(_fileMock.Object);
            _vm.ServiceLocator.RegisterService(_mapMock.Object);
        }

        [Test]
        public void Test_NoMapCallIfNoValidJson()
        {
            _mapMock.Setup(s => s.GetMapWithStrings("", ""));
            _vm.TourJson = _invalidJson;

            _vm.ImportCommand.Execute(new object());

            _mapMock.Verify(s => s.GetMapWithStrings(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(4));
        }

        [Test]
        public void Test_NoDbCallIfNoValidJson()
        {
            string imagePath = "";
            _databaseMock.Setup(s => s.AddTour(new Tour(), out imagePath));
            _fileMock.Setup(s => s.SaveImage(imagePath, new BitmapImage()));
            _vm.TourJson = _invalidJson;

            _vm.ImportCommand.Execute(new object());

            _databaseMock.Verify(s => s.AddTour(new Tour(), out imagePath), Times.Never);
            _fileMock.Verify(s => s.SaveImage(imagePath, new BitmapImage()), Times.Never);
        }

        [Test]
        public void Test_MapCallIfJsonIsValid()
        {
            _mapMock.Setup(s => s.GetMapWithStrings(It.IsAny<string>(), It.IsAny<string>()));
            string imagePath = "";
            _databaseMock.Setup(s => s.AddTour(It.IsAny<Tour>(), out imagePath)).Returns(true);
            _fileMock.Setup(s => s.SaveImage(imagePath, It.IsAny<BitmapImage>())).Returns(true);
            _vm.TourJson = _validJson;

            _vm.ImportCommand.Execute(_validJson);

            _mapMock.Verify(s => s.GetMapWithStrings(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Test]
        public void Test_DbCallIfValidJson()
        {
            string imagePath = "";
            _databaseMock.Setup(s => s.AddTour(It.IsAny<Tour>(), out imagePath)).Returns(true);
            _fileMock.Setup(s => s.SaveImage(imagePath, It.IsAny<BitmapImage>())).Returns(true);
            _vm.TourJson = _validJson;

            _vm.ImportCommand.Execute(new object());

            _databaseMock.Verify(s => s.AddTour(It.IsAny<Tour>(), out imagePath), Times.AtLeastOnce);
            _fileMock.Verify(s => s.SaveImage(imagePath, It.IsAny<BitmapImage>()), Times.AtLeastOnce);
        }
    }
}
