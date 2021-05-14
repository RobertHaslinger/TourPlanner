using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Helper.Factory;

namespace TourPlanner.Helper
{
    public class WindowFactoryLocator
    {
        private readonly Dictionary<string, IWindowFactory> _factories = new Dictionary<string, IWindowFactory>();

        public IWindowFactory GetFactory(string key)
        {
            lock (_factories)
            {
                if (_factories.ContainsKey(key))
                    return _factories[key];
            }
            return null;
        }

        public bool RegisterFactory(IWindowFactory factory, string key, bool overwriteIfExists = false)
        {
            lock (_factories)
            {
                if (!_factories.ContainsKey(key))
                {
                    _factories.Add(key, factory);
                    return true;
                }

                if (!overwriteIfExists) return false;
                _factories[key] = factory;
                return true;
            }
        }
    }
}
