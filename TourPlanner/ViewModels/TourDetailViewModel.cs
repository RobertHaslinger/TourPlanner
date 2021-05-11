using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.Services.Database;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels
{
    public class TourDetailViewModel : BaseViewModel
    {
        private IDatabaseService _databaseService => GetService<IDatabaseService>();
        private IFileService _fileService => GetService<IFileService>();

        private Tour _tour;

        public Tour Tour
        {
            get { return _tour; }
            set { _tour = value; OnPropertyChanged();}
        }

    }
}
