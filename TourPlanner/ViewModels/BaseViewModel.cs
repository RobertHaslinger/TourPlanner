using TourPlanner.Helper;

namespace TourPlanner.ViewModels
{
    public class BaseViewModel : NotifyPropertyChangedBase
    {
        private ServiceLocator _serviceLocator = new ServiceLocator();

        public ServiceLocator ServiceLocator => _serviceLocator;

        public T GetService<T>()
        {
            return _serviceLocator.GetService<T>();
        }
    }
}
