using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MDAProject.Web.Data;
using MDAProject.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using MDAProject.Web.Models;
using MDAProject.Web.Helpers;

namespace MDAProject.Web.Controllers
{
    [Authorize(Roles = "Manager, WareHouseManager")]
    public class WarehousesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public WarehousesController(
            DataContext context,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper)
        {
            _dataContext = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Warehouses.ToListAsync());
        }

        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var warehouse = await _dataContext.Warehouses
                .Include(i => i.Inventories)
                .ThenInclude(wm => wm.WarehouseManager)
                .ThenInclude(u => u.User)
                .Include(i => i.Inventories)
                .ThenInclude(d => d.Devices)
                .ThenInclude(b => b.Brand)
                .ThenInclude(d => d.Devices)
                .ThenInclude(dt => dt.DeviceType)
                .ThenInclude(d => d.Devices)
                .ThenInclude(m => m.Movements)
                .ThenInclude(mt => mt.MovementType)
                .FirstOrDefaultAsync(m => m.Id == id);            
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        public async Task<IActionResult> DetailsInventory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var warehouse = await _dataContext.Inventories
                .Include(i => i.Warehouse)
                .Include(wm => wm.WarehouseManager)
                .ThenInclude(u => u.User)
                .Include(i => i.Devices)               
                .ThenInclude(b => b.Brand)
                .ThenInclude(d => d.Devices)
                .ThenInclude(dt => dt.DeviceType)
                .ThenInclude(d => d.Devices)
                .ThenInclude(m => m.Movements)
                .ThenInclude(mt => mt.MovementType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        public async Task<IActionResult> AddInventory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _dataContext.Inventories
                .Include(i => i.Warehouse)
                .Include(wm => wm.WarehouseManager)
                .ThenInclude(u => u.User)                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var inventory = new Inventory
                {
                    DateInventary = DateTime.Today,
                    Warehouse = data.Warehouse,
                    WarehouseManager = data.WarehouseManager
                };
                _dataContext.Inventories.Add(inventory);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{id}");
            }

            return RedirectToAction($"{nameof(Details)}/{id}");
        }

        public async Task<IActionResult> AddDeviceToInventory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _dataContext.Inventories
                .Include(i => i.Warehouse)
                .Include(wm => wm.WarehouseManager)
                .ThenInclude(u => u.User)
                .Include(i => i.Devices)
                .ThenInclude(b => b.Brand)
                .ThenInclude(d => d.Devices)
                .ThenInclude(dt => dt.DeviceType)
                .ThenInclude(d => d.Devices)
                .ThenInclude(m => m.Movements)
                .ThenInclude(mt => mt.MovementType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            var model = new DeviceViewModel
            {
                WareHouseId = inventory.Warehouse.Id,
                WareHouseManagerId = inventory.WarehouseManager.Id,
                DateInventary = DateTime.Today,
                ListDevices = _combosHelper.GetComboDevices(),
                ListBrands = _combosHelper.GetComboBrands(),
                ListMovements = _combosHelper.GetComboMovementTypes(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeviceToInventory(DeviceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var inventory = await _converterHelper.ToInventoryAsync(model, true);                
                _dataContext.Inventories.Add(inventory);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.Warehouse.Id}");
            }

            //model.Lessees = _combosHelper.GetComboLessees();
            return View(model);
        }



        // GET: Warehouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WarehouseName")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(warehouse);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _dataContext.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WarehouseName")] Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(warehouse);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(warehouse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _dataContext.Warehouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouse = await _dataContext.Warehouses.FindAsync(id);
            _dataContext.Warehouses.Remove(warehouse);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseExists(int id)
        {
            return _dataContext.Warehouses.Any(e => e.Id == id);
        }
    }
}
