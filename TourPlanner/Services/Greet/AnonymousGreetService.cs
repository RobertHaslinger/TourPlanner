using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Services.Greet
{
    public class AnonymousGreetService : IGreetService
    {
        public string Greet()
        {
            return "Welcome to Tour Planner";
        }
    }
}
