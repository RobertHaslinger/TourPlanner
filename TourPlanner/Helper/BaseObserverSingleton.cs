using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Helper.Observer;

namespace TourPlanner.Helper
{
    public sealed class BaseObserverSingleton
    {
        private static readonly object _lock = new object();
        private static BaseObserverSingleton _instance = null;
        public List<IObserver> TourObservers;

        private BaseObserverSingleton()
        {
            TourObservers = new List<IObserver>();
        }

        public static BaseObserverSingleton GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BaseObserverSingleton();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
