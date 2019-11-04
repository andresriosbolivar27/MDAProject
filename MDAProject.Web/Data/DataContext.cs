using MDAProject.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Assistant> Assistants { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<DeviceType> DeviceTypes { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Movement> Movements { get; set; }

        public DbSet<MovementType> MovementTypes { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<WareHouseManager> WareHouseManagers { get; set; }
    }
}
