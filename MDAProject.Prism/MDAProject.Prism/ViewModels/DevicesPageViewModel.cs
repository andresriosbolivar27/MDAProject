using MDAProject.Common.Helpers;
using MDAProject.Common.Models;
using MDAProject.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MDAProject.Prism.ViewModels
{
    public class DevicesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ICollection<DeviceResponse> _listDevices;
        private DeviceResponse _device;
        private ObservableCollection<DeviceResponse> _devices;
        public DevicesPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _listDevices = JsonConvert.DeserializeObject<ICollection<DeviceResponse>>(Settings.Devices);
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Devices";
            
            LoadDevices();

        }

        public ObservableCollection<DeviceResponse> Devices
        {
            get => _devices;
            set => SetProperty(ref _devices, value);
        }

        

        private void LoadDevices()
        {
            Devices = new ObservableCollection<DeviceResponse>(_listDevices.Select(d => new DeviceItemViewModel(_navigationService)
            {
                Brand = d.Brand,
                CodeIntegral = d.CodeIntegral,
                CodeValorar = d.CodeValorar,
                Description = d.Description,
                DeviceType = d.DeviceType,
                IsActive = d.IsActive,
                SerialNumber = d.SerialNumber
            }).ToList());
        }
    }
}
