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
    public class EditTourViewFactory : IWindowFactory
    {
        public Window CreateWindow(Dictionary<string, object> parameters = null)
        {
            EditTourView window = new EditTourView();

            if (window.DataContext is EditTourViewModel vm && window.DataContext is ISubject subject)
            {
                BaseObserverSingleton.GetInstance.TourObservers.ForEach(subject.Attach);
                if (parameters != null)
                {
                    Tour tour = parameters.ContainsKey("tour") ? (Tour)parameters["tour"] : new Tour();
                    vm.InitTour(tour);
                }
            }

            return window;
        }
    }
}
