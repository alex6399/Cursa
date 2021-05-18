using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Entities;

namespace Cursa.Controllers
{
    public class ModuleTypesController : Controller
    {
        private readonly EfDbContext _context;

        public ModuleTypesController(EfDbContext context)
        {
            _context = context;
        }

        // GET: ModuleTypes
        public async Task<IActionResult> Index()
        {
            var efDbContext = _context.ModulesTypes.Include(m => m.ModuleSubTypes);
            return View(await efDbContext.ToListAsync());
        }

        // GET: ModuleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleType = await _context.ModulesTypes
                .Include(m => m.ModuleSubTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moduleType == null)
            {
                return NotFound();
            }

            return View(moduleType);
        }

        // GET: ModuleTypes/Create
        public IActionResult Create()
        {
            ViewData["ModuleSubTypesId"] = new SelectList(_context.ModulesSubTypes, "Id", "Name");
            return View();
        }

        // POST: ModuleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,CountChanel,ModuleSubTypesId,Description,CreatedDate,Id")] ModuleType moduleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moduleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleSubTypesId"] = new SelectList(_context.ModulesSubTypes, "Id", "Name", moduleType.ModuleSubTypesId);
            return View(moduleType);
        }

        // GET: ModuleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleType = await _context.ModulesTypes.FindAsync(id);
            if (moduleType == null)
            {
                return NotFound();
            }
            ViewData["ModuleSubTypesId"] = new SelectList(_context.ModulesSubTypes, "Id", "Name", moduleType.ModuleSubTypesId);
            return View(moduleType);
        }

        // POST: ModuleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Code,CountChanel,ModuleSubTypesId,Description,CreatedDate,Id")] ModuleType moduleType)
        {
            if (id != moduleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleTypeExists(moduleType.Id))
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
            ViewData["ModuleSubTypesId"] = new SelectList(_context.ModulesSubTypes, "Id", "Name", moduleType.ModuleSubTypesId);
            return View(moduleType);
        }

        // GET: ModuleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleType = await _context.ModulesTypes
                .Include(m => m.ModuleSubTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moduleType == null)
            {
                return NotFound();
            }

            return View(moduleType);
        }

        // POST: ModuleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moduleType = await _context.ModulesTypes.FindAsync(id);
            _context.ModulesTypes.Remove(moduleType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleTypeExists(int id)
        {
            return _context.ModulesTypes.Any(e => e.Id == id);
        }
    }
}
