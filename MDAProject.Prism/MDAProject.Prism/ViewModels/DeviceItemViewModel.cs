using MDAProject.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDAProject.Prism.ViewModels
{
    
    public class DeviceItemViewModel: DeviceResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectDeviceCommand;

        public DeviceItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectDeviceCommand => _selectDeviceCommand ?? (_selectDeviceCommand = new DelegateCommand(SelectDevice));

        private async void SelectDevice()
        {
            var parameters = new NavigationParameters
            {
                { "device", this }
            };

            await _navigationService.NavigateAsync("DevicePage", parameters);
        }

    }
}
