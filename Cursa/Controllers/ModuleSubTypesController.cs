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
    public class ModuleSubTypesController : Controller
    {
        private readonly EfDbContext _context;

        public ModuleSubTypesController(EfDbContext context)
        {
            _context = context;
        }

        // GET: ModuleSubTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ModulesSubTypes.ToListAsync());
        }

        // GET: ModuleSubTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleSubTypes = await _context.ModulesSubTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moduleSubTypes == null)
            {
                return NotFound();
            }

            return View(moduleSubTypes);
        }

        // GET: ModuleSubTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModuleSubTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ModuleSubTypes moduleSubTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moduleSubTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moduleSubTypes);
        }

        // GET: ModuleSubTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleSubTypes = await _context.ModulesSubTypes.FindAsync(id);
            if (moduleSubTypes == null)
            {
                return NotFound();
            }
            return View(moduleSubTypes);
        }

        // POST: ModuleSubTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ModuleSubTypes moduleSubTypes)
        {
            if (id != moduleSubTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleSubTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleSubTypesExists(moduleSubTypes.Id))
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
            return View(moduleSubTypes);
        }

        // GET: ModuleSubTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleSubTypes = await _context.ModulesSubTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moduleSubTypes == null)
            {
                return NotFound();
            }

            return View(moduleSubTypes);
        }

        // POST: ModuleSubTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moduleSubTypes = await _context.ModulesSubTypes.FindAsync(id);
            _context.ModulesSubTypes.Remove(moduleSubTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleSubTypesExists(int id)
        {
            return _context.ModulesSubTypes.Any(e => e.Id == id);
        }
    }
}
