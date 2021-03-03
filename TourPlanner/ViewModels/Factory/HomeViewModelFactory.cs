using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;
using TourPlanner.Services;

namespace TourPlanner.ViewModels.Factory
{
    public class HomeViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            HomeViewModel vm= new HomeViewModel();
            vm.ServiceLocator.RegisterService<IGreetService>(new AnonymousGreetService());
            return vm;
        }
    }
}
