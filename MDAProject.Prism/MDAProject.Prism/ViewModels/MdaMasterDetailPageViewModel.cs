using MDAProject.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MDAProject.Prism.ViewModels
{
    
    public class MdaMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public MdaMasterDetailPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_home",
                    PageName = "WareHouseTabbedPage",
                    Title = "Warehouse"
                },

                new Menu
                {
                    Icon = "ic_list_alt",
                    PageName = "DevicesPage",
                    Title = "Devices"
                },                

                new Menu
                {
                    Icon = "ic_map",
                    PageName = "MapPage",
                    Title = "Map"
                },

                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Logout"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
