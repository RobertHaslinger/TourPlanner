using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.ViewModels
{
    public class CreateTourViewModel : BaseViewModel
    {
        #region Properties

        private string _start;

        public string StartLocation
        {
            get { return _start; }
            set { _start = value; OnPropertyChanged();}
        }

        private string _end;

        public string EndLocation
        {
            get { return _end; }
            set { _end = value; OnPropertyChanged();}
        }

        #endregion
    }
}
