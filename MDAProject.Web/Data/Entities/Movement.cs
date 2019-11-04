using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Data.Entities
{
    public class Movement
    {
        public int Id { get; set; }

        public Device Device { get; set; }
        public Assistant Assistant { get; set; }
        public WareHouseManager WarehouseManager { get; set; }

        public MovementType MovementType { get; set; }

        public DateTime DateMovement { get; set; }
    }
}
