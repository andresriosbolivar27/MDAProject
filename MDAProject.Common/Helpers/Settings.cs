using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDAProject.Common.Helpers
{
    public class Settings
    {
        private const string _inventory = "_inventory";
        private const string _devices = "_devices";
        private const string _token = "token";
        private const string _warehousemanager = "_warehousemanager";
        private const string _isRemembered = "IsRemembered";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Inventory
        {
            get => AppSettings.GetValueOrDefault(_inventory, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_inventory, value);
        }

        public static string Devices
        {
            get => AppSettings.GetValueOrDefault(_devices, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_devices, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static string WareHouseManager
        {
            get => AppSettings.GetValueOrDefault(_warehousemanager, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_warehousemanager, value);
        }

        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }
    }
}
