using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Services.LocalFiles;

namespace TourPlanner.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private IFileService _fileService => GetService<IFileService>();

        public ICommand ViewLogCommand => new RelayCommand(ViewLogs);
        public ICommand ClearLogCommand => new RelayCommand(ClearLogs);

        private void ClearLogs(object obj)
        {
            if (_fileService.ClearLogFile())
            {
                MessageBox.Show("Logs cleared", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Log file could not be deleted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewLogs(object obj)
        {
            string logs = _fileService.ReadLogFile();
            MessageBox.Show(logs, "Logs", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
