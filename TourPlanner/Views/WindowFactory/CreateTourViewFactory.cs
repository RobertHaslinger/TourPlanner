using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;
using TourPlanner.Helper.Observer;
using TourPlanner.ViewModels;

namespace TourPlanner.Views.WindowFactory
{
    public class CreateTourViewFactory : IWindowFactory
    {
        public Window CreateWindow(Dictionary<string, object> parameters = null)
        {
            CreateTourView window = new CreateTourView();
            
            if (window.DataContext is BaseViewModel && window.DataContext is ISubject)
            {
                if (parameters != null)
                {
                    List<IObserver> observers = parameters["observers"] as List<IObserver>;
                    observers?.ForEach(((ISubject)window.DataContext).Attach);
                }
            }

            return window;
        }
    }
}
