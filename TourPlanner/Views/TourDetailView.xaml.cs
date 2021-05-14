using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourPlanner.CustomControls;
using TourPlanner.Models;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for TourDetailView.xaml
    /// </summary>
    public partial class TourDetailView
    {
        public TourDetailView()
        {
            InitializeComponent();
        }

        private void HandleEditClick(object sender, RoutedEventArgs e)
        {
            TourSelectedEventArgs eventArgs = new TourSelectedEventArgs(TourEditedEvent);
            eventArgs.SelectedTour = Tour;
            RaiseEvent(eventArgs);
        }

        private void HandleDeleteClick(object sender, RoutedEventArgs e)
        {
            TourSelectedEventArgs eventArgs = new TourSelectedEventArgs(TourDeletedEvent);
            eventArgs.SelectedTour = Tour;
            RaiseEvent(eventArgs);
        }

        private void HandleCopyClick(object sender, RoutedEventArgs e)
        {
            TourSelectedEventArgs eventArgs = new TourSelectedEventArgs(TourCopiedEvent);
            eventArgs.SelectedTour = Tour;
            RaiseEvent(eventArgs);
        }
    }
}
