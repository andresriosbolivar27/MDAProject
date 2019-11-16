using Prism;
using Prism.Ioc;
using MDAProject.Prism.ViewModels;
using MDAProject.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using MDAProject.Common.Models;
using MDAProject.Common.Helpers;
using System;
using MDAProject.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MDAProject.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTU2MDkwQDMxMzcyZTMyMmUzMGp0RjlKamdCS1g4a1kvYllaWDNzNm9EWFArTWQzbzVZUGhCbVhNUUs3VlU9"); InitializeComponent();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            {
                await NavigationService.NavigateAsync("/MdaMasterDetailPage/NavigationPage/WareHouseTabbedPage");
                //await NavigationService.NavigateAsync("LoginPage");
            }
            else
            {
                await NavigationService.NavigateAsync("LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<WareHouseTabbedPage, WareHouseTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<InventoryPage, InventoryPageViewModel>();
            containerRegistry.RegisterForNavigation<DevicesPage, DevicesPageViewModel>();
            containerRegistry.RegisterForNavigation<MdaMasterDetailPage, MdaMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
        }
    }
}
