using MDAProject.Common.Models;
using MDAProject.Web.Data;
using MDAProject.Web.Data.Entities;
using MDAProject.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WareHouseManagersController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public WareHouseManagersController(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        [HttpPost]
        [Route("GetWareHouseManagerByEmail")]
        public async Task<IActionResult> GetWareHouseManager(EmailRequest emailRequest)
        {
            try
            {
                var user = await _userHelper.GetUserByEmailAsync(emailRequest.Email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                if (await _userHelper.IsUserInRoleAsync(user, "WareHouseManager"))
                {
                    return await GetWareHouseManagerAsync(emailRequest);
                }
                else
                {
                    return await GetWareHouseManagerAsync(emailRequest);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("GetDevicesByWareHouse/{id}")]
        
        public async Task<IActionResult> GetDevices([FromRoute] int id)
        {
            var device = await _dataContext.Devices
                .Include(dt => dt.DeviceType)
                .Include(b => b.Brand)
                .Include(i => i.Inventory)
                .ThenInclude(w => w.Warehouse)
                .Where(d => d.Warehouse.Id == id).ToListAsync();
            if (device.Count == 0)
            {
                return BadRequest("Device not found.");
            }

            var response = new List<DeviceResponse>(device.Select(d => new DeviceResponse
            {
                Id = d.Id,
                Brand = d.Brand.Name,
                CodeIntegral = d.CodeIntegral,
                CodeValorar = d.CodeValorar,
                Description = d.Description,
                DeviceImages = d.DeviceImages?.Select(pi => new DeviceImageResponse
                {
                    Id = pi.Id,
                    ImageUrl = pi.ImageFullPath
                }).ToList(),
                DeviceType = d.DeviceType.DeviceTypeName,
                IsActive = d.IsActive,
                SerialNumber = d.SerialNumber

            }).ToList());
            return Ok(response);
        }




        private async Task<IActionResult> GetWareHouseManagerAsync(EmailRequest emailRequest)
        {
            var warehousemanager = await _dataContext.WareHouseManagers
                .Include(u => u.User)
                .Include(i => i.Inventories)
                .ThenInclude(w => w.Warehouse)
                .Include(i => i.Inventories)
                .ThenInclude(d => d.Devices)
                .ThenInclude(b => b.Brand)
                .ThenInclude(d => d.Devices)
                .ThenInclude(dt => dt.DeviceType)
                .ThenInclude(d => d.Devices)
                .ThenInclude(d => d.Movements)
                .ThenInclude(mt => mt.MovementType)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower().Equals(emailRequest.Email.ToLower()));

            var response = new WareHouseManagerResponse
            {
                RoleId = 2,
                Id = warehousemanager.Id,
                FirstName = warehousemanager.User.FirstName,
                LastName = warehousemanager.User.LastName,
                Address = warehousemanager.User.Address,
                Document = warehousemanager.User.Document,
                Email = warehousemanager.User.Email,
                PhoneNumber = warehousemanager.User.PhoneNumber,
                Inventories = warehousemanager.Inventories?.Select(i => new InventoryResponse
                {
                    Id = i.Id,
                    DateInventory = i.DateInventary,
                    Warehouse = ToWareHouseResponse(i.Warehouse),
                    Devices = i.Devices?.Select(d => new DeviceResponse
                    {
                        Brand = d.Brand.Name,
                        CodeIntegral = d.CodeIntegral,
                        CodeValorar = d.CodeValorar,
                        Description = d.Description,
                        DeviceImages = d.DeviceImages?.Select(pi => new DeviceImageResponse
                        {
                            Id = pi.Id,
                            ImageUrl = pi.ImageFullPath
                        }).ToList(),
                        DeviceType = d.DeviceType.DeviceTypeName,
                        Id = d.Id,
                        Movements = d.Movements?.Select(m => new MovementResponse 
                        {
                            DateMovement = m.DateMovement,
                            
                            MovementType = m.MovementType.MovementTypeName,
                            Responsible = m.WarehouseManager.User.FullName
                        }).ToList(),
                        SerialNumber = d.SerialNumber
                    }).ToList(),
                   

                }).ToList()
            };

            return Ok(response);
        }

        private WareHouseResponse ToWareHouseResponse(Warehouse warehouse)
        {            
            return new WareHouseResponse
            {                
                WarehouseName = warehouse.WarehouseName
                
            };
        }


        private DeviceResponse ToDeviceResponse(Device device)
        {
            return new DeviceResponse
            {
                Brand = device.Brand.Name,
                CodeIntegral = device.CodeIntegral,
                CodeValorar  = device.CodeValorar,
                Description = device.Description ,
                DeviceImages = device.DeviceImages?.Select(pi => new DeviceImageResponse
                {
                    Id = pi.Id,
                    ImageUrl = pi.ImageFullPath
                }).ToList(),
                DeviceType = device.DeviceType.DeviceTypeName,
                SerialNumber = device.SerialNumber,
            };
        }
        

        private BrandResponse ToBrandResponse(Brand brand)
        { 
            return new BrandResponse
            {
                BrandName = brand.Name
            };
        }

        private WareHouseManagerResponse ToWareHouseManager(WareHouseManager wareHouseManager)
        {
            return new WareHouseManagerResponse
            {
                Address = wareHouseManager.User.Address,
                Document = wareHouseManager.User.Document,
                Email = wareHouseManager.User.Email,
                FirstName = wareHouseManager.User.FirstName,
                LastName = wareHouseManager.User.LastName,
                PhoneNumber = wareHouseManager.User.PhoneNumber
            };
        }
    }
}
