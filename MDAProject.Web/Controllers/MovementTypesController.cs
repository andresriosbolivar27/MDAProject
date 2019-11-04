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
    public class MovementTypesController : Controller
    {
        private readonly DataContext _context;

        public MovementTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: MovementTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovementTypes.ToListAsync());
        }

        // GET: MovementTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementType = await _context.MovementTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movementType == null)
            {
                return NotFound();
            }

            return View(movementType);
        }

        // GET: MovementTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovementTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovementTypeName")] MovementType movementType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movementType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movementType);
        }

        // GET: MovementTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementType = await _context.MovementTypes.FindAsync(id);
            if (movementType == null)
            {
                return NotFound();
            }
            return View(movementType);
        }

        // POST: MovementTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovementTypeName")] MovementType movementType)
        {
            if (id != movementType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movementType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovementTypeExists(movementType.Id))
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
            return View(movementType);
        }

        // GET: MovementTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementType = await _context.MovementTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movementType == null)
            {
                return NotFound();
            }

            return View(movementType);
        }

        // POST: MovementTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movementType = await _context.MovementTypes.FindAsync(id);
            _context.MovementTypes.Remove(movementType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovementTypeExists(int id)
        {
            return _context.MovementTypes.Any(e => e.Id == id);
        }
    }
}
