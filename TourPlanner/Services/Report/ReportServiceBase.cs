using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Services.Report
{
    public abstract class ReportServiceBase
    {
        protected string DirPath;
        protected void GuaranteeFileAccess()
        {
            string dir = Path.GetDirectoryName(DirPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
