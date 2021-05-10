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

        public ICommand SelectedTourChangedCommand => new RelayCommand(OnSelectedTourChangedCommandExecuted);

        private static void OnSelectedTourChangedCommandExecuted(object obj)
        {
            Tour selectedTour = (Tour) obj;
            MessageBox.Show($"Tour name: {selectedTour.Name}", "Tour clicked", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private void LoadGreetMessage()
        {
            GreetMessage = GetService<IGreetService>().Greet();
        }
    }
}
