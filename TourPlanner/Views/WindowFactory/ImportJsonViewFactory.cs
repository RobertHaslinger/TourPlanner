using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper;
using TourPlanner.Helper.Factory;
using TourPlanner.Helper.Observer;
using TourPlanner.ViewModels;

namespace TourPlanner.Views.WindowFactory
{
    public class ImportJsonViewFactory : IWindowFactory
    {
        public Window CreateWindow(Dictionary<string, object> parameters = null)
        {
            CreateTourView window = new CreateTourView();

            if (window.DataContext is BaseViewModel && window.DataContext is ISubject subject)
            {
                BaseObserverSingleton.GetInstance.TourObservers.ForEach(subject.Attach);
            }

            return window;
        }
    }
}
