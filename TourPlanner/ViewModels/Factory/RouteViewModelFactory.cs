using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;

namespace TourPlanner.ViewModels.Factory
{
    public class RouteViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            RouteViewModel vm = new RouteViewModel();
            return vm;
        }
    }
}
