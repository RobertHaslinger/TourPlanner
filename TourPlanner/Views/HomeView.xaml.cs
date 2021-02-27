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
using TourPlanner.Services;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        private HomeViewModel vm;
        public HomeView(IGreetService greetService)
        {
            vm = new HomeViewModel(NavigationService, greetService);
            Initialized += (sender, args) => vm.OnNavigatedTo(sender, args);
            DataContext = vm;
            InitializeComponent();
        }
    }
}
