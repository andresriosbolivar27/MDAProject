using MDAProject.Web.Data.Entities;
using MDAProject.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1026147421", "Andres", "rios", "blackdrive3@gmail.com", "311 667 9059", "Margaritas", "Manager");
            var assistant = await CheckUserAsync("92092704302", "Andres", "Bolivar", "nacional12344@hotmail.com", "310 452 4681", "La Rivera", "Assistant");
            var warehousemanager = await CheckUserAsync("15258093", "Elkin", "Rios", "nacional12344@gmail.com", "311 317 2828", "La 49", "WareHouseManager");
            await CheckManagerAsync(manager);
            await CheckWareHouseManagersAsync(warehousemanager);
            await CheckAssistantsAsync(assistant);
            await CheckDeviceTypesAsync();
            await CheckMovementTypesAsync();
            await CheckWarehouseAsync();
            await CheckBrandAsync();
            await CheckInvetoriesAsync();
            await CheckDevicesAsync();
            await CheckMovementsAsync();

        }

        private async Task CheckInvetoriesAsync()
        {
            var warehosemanager = _context.WareHouseManagers.FirstOrDefault();
            var warehose = _context.Warehouses.FirstOrDefault();

            if (!_context.Inventories.Any())
            {
                AddInventory(warehose, warehosemanager);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckWarehouseAsync()
        {
            if (!_context.Warehouses.Any())
            {
                _context.Warehouses.Add(new Warehouse { WarehouseName = "VUR" });
                _context.Warehouses.Add(new Warehouse { WarehouseName = "Bogota" });                
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckBrandAsync()
        {
            if (!_context.Brands.Any())
            {
                _context.Brands.Add(new Brand { Name = "HP" });
                _context.Brands.Add(new Brand { Name = "Lenovo" });
                _context.Brands.Add(new Brand { Name = "Dell" });
                _context.Brands.Add(new Brand { Name = "Compaq" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckDeviceTypesAsync()
        {
            if (!_context.DeviceTypes.Any())
            {
                _context.DeviceTypes.Add(new DeviceType { DeviceTypeName = "Portatil" });
                _context.DeviceTypes.Add(new DeviceType { DeviceTypeName = "Escritorio" });
                _context.DeviceTypes.Add(new DeviceType { DeviceTypeName = "Workstation" });
                _context.DeviceTypes.Add(new DeviceType { DeviceTypeName = "Monitor" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckMovementTypesAsync()
        {
            if (!_context.MovementTypes.Any())
            {
                _context.MovementTypes.Add(new MovementType { MovementTypeName = "Entrada" });
                _context.MovementTypes.Add(new MovementType { MovementTypeName = "Salida" });
                _context.MovementTypes.Add(new MovementType { MovementTypeName = "Prestamo" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckWareHouseManagersAsync(User user)
        {
            if (!_context.WareHouseManagers.Any())
            {
                _context.WareHouseManagers.Add(new WareHouseManager { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckAssistantsAsync(User user)
        {
            if (!_context.Assistants.Any())
            {
                _context.Assistants.Add(new Assistant { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }
        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Assistant");
            await _userHelper.CheckRoleAsync("WareHouseManager");
        }

        private async void AddInventory(Warehouse warehouse,WareHouseManager warehousemanager)
        {    
            _context.Inventories.Add(new Inventory
            {
                DateInventary = DateTime.Today,
                Warehouse = warehouse,
                WarehouseManager = warehousemanager                
            });

            await _context.SaveChangesAsync();
        }

        private async Task CheckDevicesAsync()
        {
            var inventory = _context.Inventories.FirstOrDefault();
            var brand = _context.Brands.FirstOrDefault();
            var deviceType = _context.DeviceTypes.FirstOrDefault();
            var warehouse = _context.Warehouses.FirstOrDefault();
            if (!_context.Devices.Any())
            {
                AddDevice("INTEGRAL-43203", "6558","MXL2564S46D","Computador con Suite Autodesk",true,inventory,brand,deviceType,warehouse);
                AddDevice("INTEGRAL-43424", "7256","MXL452S46DS", "Computador publico piso 9", true, inventory, brand, deviceType, warehouse);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckMovementsAsync()
        {
            var device = _context.Devices.FirstOrDefault();
            var assistant = _context.Assistants.FirstOrDefault();
            var wareHouseManager = _context.WareHouseManagers.FirstOrDefault();
            var movementType = _context.MovementTypes.FirstOrDefault();
            if (!_context.Movements.Any())
            {
                AddMovement(device,assistant,wareHouseManager,movementType);
                await _context.SaveChangesAsync();
            }
        }

        private void AddMovement(Device device, Assistant assistant, WareHouseManager wareHouseManager, MovementType movementType)
        {
            _context.Movements.Add(new Movement
            {
                Device = device,
                Assistant = assistant,
                WarehouseManager = wareHouseManager,
                MovementType = movementType,
                DateMovement = DateTime.Today
            });
        }

        private void AddDevice(
            string codeIntegral,
            string codeValorar,
            string serialNumber, 
            string description, 
            bool isactive, 
            Inventory inventory, 
            Brand brand, 
            DeviceType deviceType, 
            Warehouse warehouse)
        {
            _context.Devices.Add(new Device
            {
                CodeIntegral = codeIntegral,
                CodeValorar = codeValorar,
                SerialNumber = serialNumber,
                Description = description,
                IsActive = isactive,
                Inventory = inventory,
                Brand = brand,
                DeviceType = deviceType,
                Warehouse = warehouse
            });
        }

    }
}
