using System.Configuration;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Models;
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

        private Tour _selectedTour;

        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set { _selectedTour = value; OnPropertyChanged();}
        }


        public ICommand SelectedTourChangedCommand => new RelayCommand(OnSelectedTourChangedCommandExecuted);

        private void OnSelectedTourChangedCommandExecuted(object obj)
        {
            SelectedTour = (Tour) obj;
        }

        private void LoadGreetMessage()
        {
            GreetMessage = GetService<IGreetService>().Greet();
        }
    }
}
