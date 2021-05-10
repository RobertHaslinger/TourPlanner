using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TourPlanner.Helper;

namespace TourPlanner.Models.Menu
{
    public class MenuItem : NotifyPropertyChangedBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged();}
        }

        private string _iconPath;

        public string IconPath
        {
            get { return _iconPath; }
            set { _iconPath = value; OnPropertyChanged(); }
        }

        private Func<Page> _contentPage;

        public Func<Page> ContentPage
        {
            get { return _contentPage; }
            set { _contentPage = value; OnPropertyChanged();}
        }
    }
}
