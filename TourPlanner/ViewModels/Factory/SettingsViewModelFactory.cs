using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels.Factory
{
    public class SettingsViewModelFactory : IViewModelFactory
    {
        public object CreateViewModel(DependencyObject sender)
        {
            SettingsViewModel vm= new SettingsViewModel();
            vm.ServiceLocator.RegisterService<IFileService>(new FileService());
            return vm;
        }
    }
}
