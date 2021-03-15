﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper;
using TourPlanner.Helper.Factory;
using TourPlanner.Services.Direction;
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
            }
            return vm;
        }
    }
}
