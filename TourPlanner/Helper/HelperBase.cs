using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TourPlanner.Views;

namespace TourPlanner.Helper
{
    /// <summary>
    /// This static class contains various useful methods that should be accessible from everywhere
    /// </summary>
    public static class HelperBase
    {
        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(BitmapImage img)
        {
            using (var stream = new MemoryStream())
            {
                PngBitmapEncoder encoder= new PngBitmapEncoder();
                encoder.Frames.Add((BitmapFrame.Create(img)));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        public static string GetExecutiveFullPath(string relativePath)
        {
            return Path.GetFullPath(relativePath, Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }
    }
}
