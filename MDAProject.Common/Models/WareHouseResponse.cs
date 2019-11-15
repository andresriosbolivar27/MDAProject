using System;
using System.Collections.Generic;
using System.Text;

namespace MDAProject.Common.Models
{
    public class WareHouseResponse
    {
        public int Id { get; set; }
        
        public string WarehouseName { get; set; }

        public ICollection<InventoryResponse> Inventories { get; set; }
    }
}
