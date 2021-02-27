using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using TourPlanner.Annotations;

namespace TourPlanner.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private NavigationService _navigationService;
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BaseViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public virtual void OnNavigatedTo(object sender, EventArgs e)
        {

        }

        public virtual void OnNavigatedFrom(object sender, EventArgs e)
        {

        }
    }
}
