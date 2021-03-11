using System;
using System.Threading;
using System.Threading.Tasks;
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
