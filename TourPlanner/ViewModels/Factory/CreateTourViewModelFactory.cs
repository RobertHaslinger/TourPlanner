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
    public class CreateTourViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            CreateTourViewModel vm = new CreateTourViewModel();
            if (Designer.IsDesignMode)
            {
                vm.ServiceLocator.RegisterService<IMapService>(new DesignerMapService());
                vm.ServiceLocator.RegisterService<IPredictionService>(new DesignerPredictionService());
            }
            else
            {
                vm.ServiceLocator.RegisterService<IDirectionService>(new MapQuestDirectionService());
                vm.ServiceLocator.RegisterService<IPredictionService>(new MapQuestPredictionService());
                vm.ServiceLocator.RegisterService<IMapService>(new MapQuestMapService());
                vm.ServiceLocator.RegisterService<IDatabaseService>(
                    new PostgreSQLDatabaseService(ConfigurationManager.AppSettings["connection_string"]));
                vm.ServiceLocator.RegisterService<IFileService>(new FileService());
            }
            return vm;
        }
    }
}
