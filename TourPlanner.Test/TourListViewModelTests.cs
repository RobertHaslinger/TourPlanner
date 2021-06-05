using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;
using TourPlanner.ViewModels;

namespace TourPlanner.Test
{
    [TestFixture]
    public class TourListViewModelTests
    {
        private TourListViewModel _vm;
        private Mock<IDatabaseService> _databaseMock;
        private Mock<IFileService> _fileServiceMock;

        [SetUp]
        public void SetUp()
        {
            _vm = new TourListViewModel();
            _databaseMock = new Mock<IDatabaseService>();
            _fileServiceMock = new Mock<IFileService>();
            _vm.ServiceLocator.RegisterService(_databaseMock.Object);
            _vm.ServiceLocator.RegisterService(_fileServiceMock.Object);
        }

        [Test]
        public void Test_SearchChangesTours()
        {
            _databaseMock.Setup(s => s.GetTours()).Returns(new List<Tour>()
            {
                new Tour()
                {
                    Id=1,
                    Name = "Abc"
                },
                new Tour()
                {
                    Id=1,
                    Name = "Bcc"
                }
            });
            _databaseMock.Setup(s => s.GetTourLogs(1)).Returns(new List<TourLog>());
            _fileServiceMock.Setup(s => s.GetImageBytes("")).Returns(new byte[2]);
            string search = "a";

            _vm.SearchTextChangedCommand.Execute(search);

            Assert.AreEqual(1, _vm.Tours.Count);
        }
    }
}
