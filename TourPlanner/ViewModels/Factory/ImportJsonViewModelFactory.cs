using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Map;

namespace TourPlanner.ViewModels.Factory
{
    class ImportJsonViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            ImportJsonViewModel vm = new ImportJsonViewModel();
            vm.ServiceLocator.RegisterService<IMapService>(new MapQuestMapService());
            vm.ServiceLocator.RegisterService<IDatabaseService>(
                new PostgreSQLDatabaseService(ConfigurationManager.AppSettings["connection_string"]));
            vm.ServiceLocator.RegisterService<IFileService>(new FileService());
            return vm;
        }
    }
}
