using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper;
using TourPlanner.Helper.Factory;
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
                vm.ServiceLocator.RegisterService<IPredictionService>(new DesignerPredictionService());
            }
            else
            {
                vm.ServiceLocator.RegisterService<IPredictionService>(new MapQuestPredictionService());
            }
            return vm;
        }
    }
}
