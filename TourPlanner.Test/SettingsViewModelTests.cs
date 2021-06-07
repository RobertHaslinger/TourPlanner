using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Map;
using TourPlanner.ViewModels;

namespace TourPlanner.Test
{
    [TestFixture]
    public class SettingsViewModelTests
    {
        private SettingsViewModel _vm;
        private Mock<IFileService> _fileMock;

        [SetUp]
        public void SetUp()
        {
            _vm = new SettingsViewModel();
            _fileMock = new Mock<IFileService>();

            _vm.ServiceLocator.RegisterService(_fileMock.Object);
        }

        [Test]
        public void Test_CallToServiceIfViewLogsCommandExecuted()
        {
            _fileMock.Setup(s => s.ReadLogFile()).Returns("");

            _vm.ViewLogCommand.Execute(new object());

            _fileMock.Verify(s => s.ReadLogFile(), Times.Once);
        }

        [Test]
        public void Test_CallToServiceIfDeleteLogsCommandExecuted()
        {
            _fileMock.Setup(s => s.ClearLogFile());

            _vm.ClearLogCommand.Execute(new object());

            _fileMock.Verify(s => s.ClearLogFile(), Times.Once);
        }
    }
}
