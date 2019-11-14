using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MDAProject.Prism.ViewModels
{
    public class DevicesPageViewModel : ViewModelBase
    {
        public DevicesPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Devices";

        }
    }
}
