using MDAProject.Web.Data.Entities;
using MDAProject.Web.Models;
using System.Threading.Tasks;


namespace MDAProject.Web.Helpers
{
    public interface IConverterHelper
    {
        DeviceViewModel ToDeviceViewModel(Movement movement);
        Task<Inventory> ToInventoryAsync(DeviceViewModel model, bool isNew);
    }
}