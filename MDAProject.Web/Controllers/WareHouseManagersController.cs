using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MDAProject.Web.Data;
using MDAProject.Web.Data.Entities;

namespace MDAProject.Web.Controllers
{
    public class WareHouseManagersController : Controller
    {
        private readonly DataContext _dataContext;

        public WareHouseManagersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: WareHouseManagers
        public IActionResult Index()
        {
            return View(_dataContext.WareHouseManagers
                .Include(w => w.User)
                .Include(w => w.Inventories)
                .Include(w => w.Movements));
        }

        // GET: WareHouseManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wareHouseManager = await _dataContext.WareHouseManagers
                .Include(w => w.User)
                .Include(i => i.Inventories)
                .ThenInclude(d => d.Devices)
                .ThenInclude(dt => dt.DeviceType)
                .Include(m => m.Movements)
                .ThenInclude(mt => mt.MovementType)                
                .FirstOrDefaultAsync(w => w.Id == id);
            if (wareHouseManager == null)
            {
                return NotFound();
            }

            return View(wareHouseManager);
        }

        // GET: WareHouseManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WareHouseManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] WareHouseManager wareHouseManager)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(wareHouseManager);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wareHouseManager);
        }

        // GET: WareHouseManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wareHouseManager = await _dataContext.WareHouseManagers.FindAsync(id);
            if (wareHouseManager == null)
            {
                return NotFound();
            }
            return View(wareHouseManager);
        }

        // POST: WareHouseManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] WareHouseManager wareHouseManager)
        {
            if (id != wareHouseManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(wareHouseManager);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WareHouseManagerExists(wareHouseManager.Id))
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
            return View(wareHouseManager);
        }

        // GET: WareHouseManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wareHouseManager = await _dataContext.WareHouseManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wareHouseManager == null)
            {
                return NotFound();
            }

            return View(wareHouseManager);
        }

        // POST: WareHouseManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wareHouseManager = await _dataContext.WareHouseManagers.FindAsync(id);
            _dataContext.WareHouseManagers.Remove(wareHouseManager);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WareHouseManagerExists(int id)
        {
            return _dataContext.WareHouseManagers.Any(e => e.Id == id);
        }
    }
}
