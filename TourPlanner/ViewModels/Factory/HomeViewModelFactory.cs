using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;
using TourPlanner.Services;
using TourPlanner.Services.Database;
using TourPlanner.Services.Greet;
using TourPlanner.Services.LocalFiles;
using TourPlanner.Views;
using TourPlanner.Views.WindowFactory;

namespace TourPlanner.ViewModels.Factory
{
    public class HomeViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            HomeViewModel vm= new HomeViewModel();
            vm.ServiceLocator.RegisterService<IGreetService>(new AnonymousGreetService());
            vm.ServiceLocator.RegisterService<IDatabaseService>(
                new PostgreSQLDatabaseService(ConfigurationManager.AppSettings["connection_string"]));
            vm.ServiceLocator.RegisterService<IFileService>(new FileService());
            vm.WindowFactoryLocator.RegisterFactory(new EditTourViewFactory(), "EditTourViewFactory");
            return vm;
        }
    }
}
