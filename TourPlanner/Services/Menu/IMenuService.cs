using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models.Menu;

namespace TourPlanner.Services.Menu
{
    public interface IMenuService
    {
        IEnumerable<MenuItem> GetMenuItems();
        MenuItem GetMenuItemByName(string name);

    }
}
