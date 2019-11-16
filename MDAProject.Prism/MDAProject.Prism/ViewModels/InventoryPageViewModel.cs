using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MDAProject.Prism.ViewModels
{
    public class InventoryPageViewModel : ViewModelBase
    {
        public InventoryPageViewModel(
            INavigationService navigationService): base(navigationService)
        {
            Title = "Inventories";
        }
    }
}
