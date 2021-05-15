using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TourPlanner.Services.LocalFiles
{
    public class FileService : IFileService
    {
        public bool SaveImage(string path, BitmapImage image)
        {
            try
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }

        public bool ExportJson(string path, string json)
        {
            try
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.WriteAllText(path, json);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool DeleteImage(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public byte[] GetImageBytes(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
