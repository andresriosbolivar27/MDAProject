using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Data.Entities
{
    public class Assistant
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Movement> Movements { get; set; }
    }
}
