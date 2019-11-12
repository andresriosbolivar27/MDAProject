using MDAProject.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Models
{
    public class DeviceViewModel:Device
    {
        public int InventoryId { get; set; }
        public int WareHouseId { get; set; }
        public int WareHouseManagerId { get; set; }
        public int DeviceId { get; set; }
        public int BrandId { get; set; }
        public int MovementTypeId { get; set; }

        [Display(Name = "Date Inventary")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateInventary { get; set; }

        [Display(Name = "Date Inventary")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateInventaryLocal => DateInventary.ToLocalTime();
        public IEnumerable<SelectListItem> ListDevices { get; set; }
        public IEnumerable<SelectListItem> ListBrands { get; set; }
        public IEnumerable<SelectListItem> ListMovements { get; set; }



    }
}
