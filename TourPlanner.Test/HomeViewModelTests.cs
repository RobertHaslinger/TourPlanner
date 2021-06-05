using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Moq;
using NUnit.Framework;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.ViewModels;

namespace TourPlanner.Test
{
    [TestFixture]
    public class HomeViewModelTests
    {
        private HomeViewModel _vm;
        private Mock<IDatabaseService> _databaseMock;

        [SetUp]
        public void SetUp()
        {
            _vm = new HomeViewModel();
            _databaseMock = new Mock<IDatabaseService>();
            _vm.ServiceLocator.RegisterService(_databaseMock.Object);
        }

        [Test]
        public void Test_DbCallWhenDeleteCommandExecuted()
        {
            Tour tour= new Tour()
            {
                Name = "Test",
                Id = 1000
            };
            _databaseMock.Setup(s => s.DeleteTour(1000));

            _vm.SelectedTourDeletedCommand.Execute(tour);
            
            _databaseMock.Verify(s => s.DeleteTour(1000), Times.Once);
        }

        [Test]
        public void Test_DbCallWhenCopyCommandExecuted()
        {
            Tour tour = new Tour()
            {
                Name = "Test",
                StartLocation = "",
                EndLocation = "",
                Image = new BitmapImage()
            };
            string imagePath = "";
            _databaseMock.Setup(s => s.AddTour(tour, out imagePath));

            _vm.SelectedTourCopiedCommand.Execute(tour);

            _databaseMock.Verify(s => s.AddTour(It.Is<Tour>(t => t.Name == "Test"), out imagePath), Times.Once);
        }

        [Test]
        public void Test_SearchCommandChangesTourLogs()
        {
            _vm.SelectedTour= new Tour()
                {
                    Name = "Tour",
                    Logs = new ObservableCollection<TourLog>()
                    {
                        new TourLog()
                        {
                            Name = "Abc"
                        },
                        new TourLog()
                        {
                            Name = "Abb"
                        },
                        new TourLog()
                        {
                            Name = "Bcc"
                        }
                    }
                };
            string search = "a";

            _vm.SearchTextChangedCommand.Execute(search);

            Assert.AreEqual(2, _vm.SelectedTourLogs.Count);
        }
    }
}
