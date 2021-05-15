using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TourPlanner.Services.LocalFiles
{
    public interface IFileService
    {
        bool SaveImage(string path, BitmapImage image);
        bool ExportJson(string path, string json);
        bool DeleteImage(string path);

        byte[] GetImageBytes(string path);
    }
}
