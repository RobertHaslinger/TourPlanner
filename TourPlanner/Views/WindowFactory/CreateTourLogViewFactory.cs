using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper;
using TourPlanner.Helper.Factory;
using TourPlanner.Helper.Observer;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Views.WindowFactory
{
    public class CreateTourLogViewFactory : IWindowFactory
    {
        public Window CreateWindow(Dictionary<string, object> parameters = null)
        {
            CreateTourLogView window = new CreateTourLogView();

            if (window.DataContext is CreateTourLogViewModel vm && window.DataContext is ISubject subject)
            {
                vm.TourId = parameters.ContainsKey("tourId") ? (int)parameters["tourId"] : -1;
                BaseObserverSingleton.GetInstance.TourObservers.ForEach(subject.Attach);
            }

            return window;
        }
    }
}
