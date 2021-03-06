using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TourPlanner.Models.Menu
{
    public class MenuItem
    {
        public string Name { get; set; }

        public string IconPath { get; set; }
        public Func<Page> ContentPage { get; set; }
    }
}
