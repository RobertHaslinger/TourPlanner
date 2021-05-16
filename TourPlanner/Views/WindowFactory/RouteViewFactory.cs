using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TourPlanner.Helper;
using TourPlanner.Helper.Factory;
using TourPlanner.Helper.Observer;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Views.WindowFactory
{
    public class RouteViewFactory : IWindowFactory
    {
        public Window CreateWindow(Dictionary<string, object> parameters = null)
        {
            RouteView window = new RouteView();
            BitmapImage route = parameters.ContainsKey("route") ? (BitmapImage)parameters["route"] : new BitmapImage();
            window.Route = route;
            return window;
        }
    }
}
