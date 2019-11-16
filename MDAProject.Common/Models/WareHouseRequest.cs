using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MDAProject.Common.Models
{
    public class WareHouseRequest
    {
        [Required]
        public int CodeWareHouse { get; set; }
    }
}
