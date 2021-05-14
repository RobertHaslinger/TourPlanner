using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TourPlanner.Helper.Factory
{
    public interface IWindowFactory
    {
        Window CreateWindow(Dictionary<string, object> parameters = null);
    }
}
