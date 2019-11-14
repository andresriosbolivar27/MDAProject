using System;
using System.Collections.Generic;
using System.Text;

namespace MDAProject.Common.Models
{
    public class InventoryResponse
    {
        public int Id { get; set; }

        public DateTime DateInventory { get; set; }

        public DateTime DateTimeLocal => DateInventory.ToLocalTime();
        public WareHouseResponse Warehouse { get; set; }

        public WareHouseManagerResponse WareHouseManager { get; set; }

        public ICollection<DeviceResponse> Devices { get; set; }

    }
}
