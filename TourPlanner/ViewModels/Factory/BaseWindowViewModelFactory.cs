using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;
using TourPlanner.Services.Menu;

namespace TourPlanner.ViewModels.Factory
{
    public class BaseWindowViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            BaseWindowViewModel vm = new BaseWindowViewModel();
            vm.ServiceLocator.RegisterService<IMenuService>(new BaseMenuService());
            return vm;
        }
    }
}
