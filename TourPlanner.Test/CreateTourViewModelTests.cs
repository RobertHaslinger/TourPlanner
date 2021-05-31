using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Moq;
using NUnit.Framework;
using TourPlanner.DataAccessLayer.Models.Geodata;
using TourPlanner.DataAccessLayer.Models.Route;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.Direction;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Map;
using TourPlanner.ViewModels;

namespace TourPlanner.Test
{
    [TestFixture]
    public class CreateTourViewModelTests
    {
        private CreateTourViewModel _vm;
        private Mock<IDatabaseService> _databaseMock;
        private Mock<IFileService> _fileMock;
        private Mock<IMapService> _mapMock;
        private Mock<IDirectionService> _directionMock;

        [SetUp]
        public void SetUp()
        {
            _vm= new CreateTourViewModel();
            _databaseMock= new Mock<IDatabaseService>();
            _fileMock= new Mock<IFileService>();
            _mapMock= new Mock<IMapService>();
            _directionMock= new Mock<IDirectionService>();

            _vm.ServiceLocator.RegisterService(_databaseMock.Object);
            _vm.ServiceLocator.RegisterService(_fileMock.Object);
            _vm.ServiceLocator.RegisterService(_mapMock.Object);
            _vm.ServiceLocator.RegisterService(_directionMock.Object);
        }

        [Test]
        public void Test_NoPreviewIfNoValidState()
        {
            _mapMock.Setup(s => s.GetMapWithLocations(_vm.RealStartLocation, _vm.RealEndLocation));
           _directionMock.Setup(s => s.GetSimpleRoute(_vm.RealStartLocation, _vm.RealEndLocation));

            _vm.PreviewRouteCommand.Execute(new object());

           _mapMock.Verify(s => s.GetMapWithLocations(_vm.RealStartLocation, _vm.RealEndLocation), Times.Never);
           _directionMock.Verify(s => s.GetSimpleRoute(_vm.RealStartLocation, _vm.RealEndLocation), Times.Never);
        }

        [Test]
        public void Test_PreviewIfValidState()
        {
            //simulate locations have been picked
            _vm.RealStartLocation= new Location();
            _vm.RealEndLocation= new Location();
            _mapMock.Setup(s => s.GetMapWithLocations(_vm.RealStartLocation, _vm.RealEndLocation));
            _directionMock.Setup(s => s.GetSimpleRoute(_vm.RealStartLocation, _vm.RealEndLocation));

            _vm.PreviewRouteCommand.Execute(new object());

            _mapMock.Verify(s => s.GetMapWithLocations(_vm.RealStartLocation, _vm.RealEndLocation), Times.Once);
            _directionMock.Verify(s => s.GetSimpleRoute(_vm.RealStartLocation, _vm.RealEndLocation), Times.Once);
        }

        [Test]
        public void Test_NoSaveIfNoValidState()
        {
            _vm.TourName = "Test tour";
            string imagePath = "";
            _databaseMock.Setup(s => s.AddTour(new Tour(), out imagePath));
            _fileMock.Setup(s => s.SaveImage(imagePath, new BitmapImage()));

            _vm.SaveTourCommand.Execute(new object());

            _databaseMock.Verify(s => s.AddTour(new Tour(), out imagePath ), Times.Never);
            _fileMock.Verify(s => s.SaveImage(imagePath, new BitmapImage()), Times.Never);
        }

        [Test]
        public void Test_SaveIfValidState()
        {
            _vm.TourName = "Test tour";
            _vm.RealStartLocation= new Location();
            _vm.RealEndLocation= new Location();
            _vm.PreviewRoute= new Route() ;
            _vm.PreviewMap= new BitmapImage();
            string imagePath = "";
            _databaseMock.Setup(s => s.AddTour(It.Is<Tour>(t => t.Name == "Test tour"), out imagePath)).Returns(true);
            _fileMock.Setup(s => s.SaveImage(imagePath, _vm.PreviewMap)).Returns(true);

            //act and except an exception because the sender is an object rather than a valid window
            Assert.Throws<InvalidCastException>(() =>_vm.SaveTourCommand.Execute(new object()));

            _databaseMock.Verify(s => s.AddTour(It.Is<Tour>(t => t.Name == "Test tour"), out imagePath), Times.Once);
            _fileMock.Verify(s => s.SaveImage(imagePath, _vm.PreviewMap), Times.Once);
        }
    }
}
