using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper;
using TourPlanner.Helper.Factory;
using TourPlanner.Services.Database;
using TourPlanner.Services.Direction;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Map;
using TourPlanner.Services.Prediction;
using TourPlanner.Views;
using TourPlanner.Views.WindowFactory;

namespace TourPlanner.ViewModels.Factory
{
    public class TourListViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            TourListViewModel vm = new TourListViewModel();
            if (Designer.IsDesignMode)
            {
            }
            else
            {
                vm.ServiceLocator.RegisterService<IDatabaseService>(
                    new PostgreSQLDatabaseService(ConfigurationManager.AppSettings["connection_string"]));
                vm.ServiceLocator.RegisterService<IFileService>(new FileService());
                vm.WindowFactoryLocator.RegisterFactory(new CreateTourViewFactory(), "CreateTourViewFactory");
                vm.WindowFactoryLocator.RegisterFactory(new ImportJsonViewFactory(), "ImportJsonViewFactory");
            }
            return vm;
        }
    }
}
