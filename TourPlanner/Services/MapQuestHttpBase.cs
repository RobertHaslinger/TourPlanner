using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Services
{
    public class MapQuestHttpBase
    {
        protected HttpClient HttpClient = new HttpClient()
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["mq_api_base"])
        };
    }
}
