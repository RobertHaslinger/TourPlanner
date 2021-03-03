using System.Windows;

namespace TourPlanner.Helper.Factory
{
    public interface IViewModelFactory
    {
        object CreateViewModel(DependencyObject sender);
    }
}
