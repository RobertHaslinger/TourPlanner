using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TourPlanner.Models;

namespace TourPlanner.CustomControls
{
    public class TourDetailControlBase : UserControl
    {
        public static readonly DependencyProperty TourProperty = DependencyProperty.Register(
            "Tour", typeof(Tour), typeof(TourDetailControlBase), new PropertyMetadata(default(Tour)));

        public Tour Tour
        {
            get { return (Tour) GetValue(TourProperty); }
            set { SetValue(TourProperty, value); }
        }
    }
}
