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

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTextChangedEventArgs args= new SearchTextChangedEventArgs(SearchTextChangedEvent);
            args.Text = ((TextBox) sender).Text;
            RaiseEvent(args);
        }
    }
}
