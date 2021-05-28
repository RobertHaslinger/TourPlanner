using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mime;
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

        public string ReadLogFile()
        {
            try
            {
                return File.ReadAllText(ConfigurationManager.AppSettings["log_file_path"]);
            }
            catch (Exception e)
            {
                return "Log file either does not exist or is not accessible";
            }
        }

        public bool ClearLogFile()
        {
            try
            {
                File.WriteAllText(ConfigurationManager.AppSettings["log_file_path"], "Empty");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
