using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper;
using TourPlanner.Helper.Factory;

namespace TourPlanner.ViewModels
{
    public class BaseViewModel : NotifyPropertyChangedBase
    {
        private ServiceLocator _serviceLocator = new ServiceLocator();
        private WindowFactoryLocator _windowFactoryLocator = new WindowFactoryLocator();

        public ServiceLocator ServiceLocator => _serviceLocator;
        public WindowFactoryLocator WindowFactoryLocator => _windowFactoryLocator;

        public T GetService<T>()
        {
            return _serviceLocator.GetService<T>();
        }

        public IWindowFactory GetWindowFactory(string key)
        {
            return _windowFactoryLocator.GetFactory(key);
        }

        /// <summary>
        /// Source: https://stackoverflow.com/a/22453097/10888504
        /// </summary>
        /// <param name="action"></param>
        /// <param name="interval"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async Task RepeatActionAfter(Action action, TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                action();
                Task task = Task.Delay(interval, cancellationToken);

                try
                {
                    await task;
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }
    }
}
