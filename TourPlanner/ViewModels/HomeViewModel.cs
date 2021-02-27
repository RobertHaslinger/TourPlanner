using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TourPlanner.Services;

namespace TourPlanner.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        private IGreetService _greetService;

        public HomeViewModel(NavigationService navigationService, IGreetService greetService) : base(navigationService)
        {
            _greetService = greetService;
        }

        #region Properties

        private string _greetMessage;

        public string GreetMessage
        {
            get => _greetMessage;
            set { _greetMessage = value; OnPropertyChanged();}
        }

        #endregion

        public override void OnNavigatedTo(object sender, EventArgs e)
        {
            GreetMessage = _greetService.Greet();
        }
    }
}
