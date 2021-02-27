using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Services
{
    public class GreetService : IGreetService
    {
        public string Greet()
        {
            return "Hello to my awesome project";
        }
    }
}
