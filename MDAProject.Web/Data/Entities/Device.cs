using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Data.Entities
{
    public class Device
    {
        public int Id { get; set; }

        [Display(Name = "Serial Number")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string SerialNumber { get; set; }

        public string Description { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        //Relacion Muchos a uno
        public Inventory Inventory { get; set; }

        public Brand Brand { get; set; }

        public DeviceType DeviceType { get; set; }
        public Warehouse Warehouse { get; set; }

        public ICollection<Movement> Movements { get; set; }

        

    }
}
