using MDAProject.Web.Data;
using MDAProject.Web.Data.Entities;
using MDAProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public DeviceViewModel ToDeviceViewModel(Movement movement)
        {
            return new DeviceViewModel
            {
                DeviceId = movement.Device.Id,
                WareHouseManagerId = movement.Device.Inventory.WarehouseManager.Id,
                BrandId = movement.Device.Brand.Id,
                InventoryId = movement.Device.Inventory.Id,
                WareHouseId = movement.Device.Warehouse.Id,
                MovementTypeId = movement.MovementType.Id,
                DateInventary = movement.Device.Inventory.DateInventary,
                ListDevices = _combosHelper.GetComboDevices(),
                ListBrands = _combosHelper.GetComboBrands(),
                ListMovements = _combosHelper.GetComboMovementTypes(),
                CodeIntegral = movement.Device.CodeIntegral,
                CodeValorar = movement.Device.CodeValorar,
                SerialNumber = movement.Device.SerialNumber,
                Description = movement.Device.Description,
                IsActive = movement.Device.IsActive
            };
        }

        public async Task<Inventory> ToInventoryAsync(DeviceViewModel model, bool isNew)
        {
            return new Inventory
            {
                DateInventary = model.DateInventary.ToUniversalTime(),
                Id = isNew ? 0 : model.Id,
                WarehouseManager = await _dataContext.WareHouseManagers.FindAsync(model.WareHouseManagerId)
            };
        }

    }
}
