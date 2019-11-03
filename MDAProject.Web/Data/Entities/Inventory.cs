using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Data.Entities
{
    public class Inventory
    {
        public int Id { get; set; }

        [Display(Name = "Date Inventary")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateInventary { get; set; }

        [Display(Name = "Date Inventary")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateInventaryLocal => DateInventary.ToLocalTime();
        public Warehouse Warehouse { get; set; }

        [Display(Name = "Warehouse Manager")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public WarehouseManager WarehouseManager { get; set; }



    }
}
