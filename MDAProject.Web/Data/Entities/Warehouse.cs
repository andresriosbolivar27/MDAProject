using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Data.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Display(Name = "Warehouse Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string WarehouseName { get; set; }

        public ICollection<Inventory> Inventories { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
