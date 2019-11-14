using MDAProject.Common.Helpers;
using MDAProject.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;
namespace MDAProject.Prism.ViewModels
{
    public class WareHouseTabbedPageViewModel : ViewModelBase
    {
        public WareHouseTabbedPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            //var warehouse = JsonConvert.DeserializeObject<WareHouseResponse>(Settings.Inventory);
            //Title = $"Warehouse: {inventory.Warehouse}";
            Title = "VUR";
        }
    }
}
