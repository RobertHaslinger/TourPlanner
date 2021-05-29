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
using TourPlanner.Services.LocalFiles;
using TourPlanner.Services.Report;
using TourPlanner.Views.WindowFactory;

namespace TourPlanner.ViewModels.Factory
{
    public class TourDetailViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            TourDetailViewModel vm = new TourDetailViewModel();
            if (Designer.IsDesignMode)
            {
            }
            else
            {
                vm.ServiceLocator.RegisterService<IDatabaseService>(
                    new PostgreSQLDatabaseService(ConfigurationManager.AppSettings["connection_string"]));
                vm.ServiceLocator.RegisterService<IFileService>(new FileService());
                vm.ServiceLocator.RegisterService<IReportService>(new PdfReportService());
                vm.WindowFactoryLocator.RegisterFactory(new CreateTourLogViewFactory(), "CreateTourLogViewFactory");
                vm.WindowFactoryLocator.RegisterFactory(new RouteViewFactory(), "RouteViewFactory");
            }
            return vm;
        }
    }
}
