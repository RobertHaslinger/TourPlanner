using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models.Menu;
using TourPlanner.Services.Menu;

namespace TourPlanner.ViewModels
{
    public class BaseWindowViewModel : BaseViewModel
    {
        private ObservableCollection<MenuItem> _menuItems = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> MenuItems
        {
            get
            {
                if (!_menuItems.Any())
                {
                    LoadMenuItems();
                }

                return _menuItems;
            }
            //need to raise OnPropertyChanged because ObservableCollection only raises this event when items in the collection change
            set { _menuItems = value; OnPropertyChanged(); }
        }

        private void LoadMenuItems()
        {
            var menuService = GetService<IMenuService>();
            if (menuService==null) return;

            foreach (MenuItem menuItem in menuService.GetMenuItems())
            {
                _menuItems.Add(menuItem);
            }
        }
    }
}
