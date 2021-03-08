using System.Configuration;
using TourPlanner.Services;
using TourPlanner.Services.Greet;

namespace TourPlanner.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private string _greetMessage;

        public string GreetMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_greetMessage))
                {
                    LoadGreetMessage();
                }

                return _greetMessage;
            }
            set { _greetMessage = value; OnPropertyChanged(); }
        }

        private void LoadGreetMessage()
        {
            GreetMessage = GetService<IGreetService>().Greet();
        }
    }
}
