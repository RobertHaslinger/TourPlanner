using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class CreateTourLogViewModelTests
    {
        private CreateTourLogViewModel _vm;
        private Mock<IDatabaseService> _databaseMock;

        [SetUp]
        public void SetUp()
        {
            _vm = new CreateTourLogViewModel();
            _databaseMock = new Mock<IDatabaseService>();

            _vm.ServiceLocator.RegisterService(_databaseMock.Object);
        }

        [Test]
        public void Test_NoSaveWhenNotValid()
        {
            _databaseMock.Setup(s => s.AddTourLog(new TourLog()));

            _vm.SaveCommand.Execute(new object());

            _databaseMock.Verify(s => s.AddTourLog(new TourLog()), Times.Never);
        }

        [Test]
        public void Test_SaveWhenValid()
        {
            _vm.Name = "Mein neuer Log";
            _vm.TourId = 9999;
            _vm.Report = "Mein report";
            _databaseMock.Setup(s => s.AddTourLog(It.Is<TourLog>(t => t.Name=="Mein neuer Log")));

            _vm.SaveCommand.Execute(new object());

            _databaseMock.Verify(s => s.AddTourLog(It.Is<TourLog>(t => t.Name == "Mein neuer Log")), Times.Once);
        }
    }
}
