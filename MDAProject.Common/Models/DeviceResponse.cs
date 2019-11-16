using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDAProject.Common.Models
{
    public class DeviceResponse
    {
        public int Id { get; set; }

        public string CodeIntegral { get; set; }

        public string CodeValorar { get; set; }

        public string SerialNumber { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string Brand { get; set; }

        public string DeviceType { get; set; }

        public  ICollection<MovementResponse> Movements { get; set; }

        public ICollection<DeviceImageResponse> DeviceImages { get; set; }

        public string FirstImage
        {
            get
            {
                if (DeviceImages == null || DeviceImages.Count == 0)
                {
                    return "https://mdaproject.azurewebsites.net/images/Devices/noImage.png";
                }

                return DeviceImages.FirstOrDefault().ImageUrl;
            }
        }
    }
}
