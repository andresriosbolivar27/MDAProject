using System;
using System.Collections.Generic;
using System.Text;

namespace MDAProject.Common.Models
{
    public class MovementResponse
    {
        public int Id { get; set; }
       
        public string Responsible { get; set; }

        public DateTime DateMovement { get; set; }

        public DateTime DateMovementLocal => DateMovement.ToLocalTime();
        public string MovementType { get; set; }
    }
}
