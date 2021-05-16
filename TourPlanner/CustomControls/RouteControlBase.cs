using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TourPlanner.Models;

namespace TourPlanner.CustomControls
{
    public class RouteControlBase : Window
    {
        public static readonly DependencyProperty RouteProperty = DependencyProperty.Register(
            "Route", typeof(BitmapImage), typeof(RouteControlBase), new PropertyMetadata(default(BitmapImage)));

        public BitmapImage Route
        {
            get { return (BitmapImage)GetValue(RouteProperty); }
            set { SetValue(RouteProperty, value); }
        }
    }
}
