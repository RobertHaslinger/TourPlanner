using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.Test
{
    [TestFixture]
    public class TourTests
    {
        [Test]
        public void Test_TourHasSpecialtiesReturnsTrueIfAny()
        {
            Tour tour= new Tour()
            {
                Name = "Test Tour",
                HasFerry = true
            };

            Assert.IsTrue(tour.HasSpecialties());
        }

        [Test]
        public void Test_TourHasSpecialtiesReturnsFalseIfNone()
        {
            Tour tour = new Tour()
            {
                Name = "Test Tour"
            };

            Assert.IsFalse(tour.HasSpecialties());
        }

        [Test]
        public void Test_TourLoadLogs()
        {
            Tour tour= new Tour();
            Mock<IDatabaseService> _databaseMock = new Mock<IDatabaseService>();
            _databaseMock.Setup(s => s.GetTourLogs(0)).Returns(new List<TourLog> {new TourLog(), new TourLog()});

            tour.LoadLogs(_databaseMock.Object);

            Assert.AreEqual(2, tour.Logs.Count);
            _databaseMock.Verify(s => s.GetTourLogs(0), Times.Once);
        }

        [Test]
        public void Test_TourLoadImage()
        {
            Tour tour = new Tour()
            {
                ImagePath = "pack://application:,,,/Images/sample_map_600x400.png"
            };
            Mock<IFileService> _fileMock = new Mock<IFileService>();
            _fileMock.Setup(s => s.GetImageBytes("pack://application:,,,/Images/sample_map_600x400.png"));

            tour.LoadImage(_fileMock.Object);

            _fileMock.Verify(s => s.GetImageBytes("pack://application:,,,/Images/sample_map_600x400.png"), Times.Once);
        }
    }
}
