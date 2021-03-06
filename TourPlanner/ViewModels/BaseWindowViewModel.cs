using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Helper;
using TourPlanner.Services.Menu;
using MenuItem = TourPlanner.Models.Menu.MenuItem;

namespace TourPlanner.ViewModels
{
    public class BaseWindowViewModel : BaseViewModel
    {
        #region Properties

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

        private Page _currentPage;

        public Page CurrentPage
        {
            get
            {
                if (_currentPage == null)
                {
                    LoadDefaultPage();
                }

                return _currentPage;
            }
            set { _currentPage = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand MenuItemClickedCommand => new RelayCommand(NavigateToPage);

        #endregion


        #region Methods
        private void LoadMenuItems()
        {
            var menuService = GetService<IMenuService>();
            if (menuService == null) return;

            foreach (MenuItem menuItem in menuService.GetMenuItems())
            {
                _menuItems.Add(menuItem);
            }
        }

        private void LoadDefaultPage()
        {
            var menuService = GetService<IMenuService>();
            if (menuService == null) return;

            _currentPage = menuService.GetMenuItemByName("Home")?.ContentPage.Invoke();
        }



        private void NavigateToPage(object sender)
        {
            MenuItem menuItem = (MenuItem) sender;
            if (menuItem == null) return;
            var page = menuItem.ContentPage.Invoke();
            if (!(page.GetType() == CurrentPage.GetType()))
            {
                CurrentPage = menuItem.ContentPage.Invoke();
            }
        }

        #endregion


    }
}
