using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TourPlanner.Views;
using MenuItem = TourPlanner.Models.Menu.MenuItem;

namespace TourPlanner.Services.Menu
{
    public class BaseMenuService : IMenuService
    {
        private List<MenuItem> _menuItems = new List<MenuItem>()
        {
            new MenuItem()
            {
                Name = "Home",
                IconPath = "/TourPlanner;component/Images/home.png",
                ContentPage = () => new HomeView()
            },
            new MenuItem()
            {
                Name = "Settings",
                IconPath = "/TourPlanner;component/Images/settings.png",
                ContentPage = () => new SettingsView()
            }
        };
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return _menuItems;
        }

        public MenuItem GetMenuItemByName(string name)
        {
            try
            {
                return _menuItems.Find(item => item.Name == name);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
