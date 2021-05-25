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

namespace TourPlanner.ViewModels.Factory
{
    public class CreateTourLogViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            CreateTourLogViewModel vm = new CreateTourLogViewModel();
            if (Designer.IsDesignMode)
            {
            }
            else
            {
                vm.ServiceLocator.RegisterService<IDatabaseService>(
                    new PostgreSQLDatabaseService(ConfigurationManager.AppSettings["connection_string"]));
            }
            return vm;
        }
    }
}
