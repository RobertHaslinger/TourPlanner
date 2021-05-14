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
    /// Interaction logic for TourListView.xaml
    /// </summary>
    public partial class TourListView
    {
        public TourListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bubble the event to a potential parent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleTourSelected(object sender, SelectionChangedEventArgs e)
        {
            TourSelectedEventArgs eventArgs= new TourSelectedEventArgs(TourSelectedEvent);
            //catch exception if no tour is selected
            try
            {
                eventArgs.SelectedTour = e.AddedItems[0] as Tour;
            }
            catch (Exception exception)
            {
            }
            RaiseEvent(eventArgs);
        }
    }
}
