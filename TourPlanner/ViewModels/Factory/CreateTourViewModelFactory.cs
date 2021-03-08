using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;

namespace TourPlanner.ViewModels.Factory
{
    public class CreateTourViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            CreateTourViewModel vm = new CreateTourViewModel();
            return vm;
        }
    }
}
