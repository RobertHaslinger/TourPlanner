using System;
using System.Windows;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {

            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {

            }
        }
    }
}
