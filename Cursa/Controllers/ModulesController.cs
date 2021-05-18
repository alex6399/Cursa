﻿using System;
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
    public class ModulesController : Controller
    {
        private readonly EfDbContext _context;

        public ModulesController(EfDbContext context)
        {
            _context = context;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            var efDbContext = _context.Modules
                .Include(x => x.ActualOrderCard)
                .Include(x => x.CreatedUser)
                .Include(x => x.DestinationOrderCard)
                .Include(x => x.ModifiedUser)
                .Include(x => x.ModuleType);
            return View(await efDbContext.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(x => x.ActualOrderCard)
                .Include(x => x.CreatedUser)
                .Include(x => x.DestinationOrderCard)
                .Include(x => x.ModifiedUser)
                .Include(x => x.ModuleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            ViewData["ActualOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name");
            ViewData["CreatedUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["DestinationOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name");
            ViewData["ModifiedUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ModuleTypeId"] = new SelectList(_context.ModulesTypes, "Id", "Code");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleTypeId,DestinationOrderCardId,SerialNumber,Place,IsInstalled,ActualOrderCardId,ManufacturingData,CreatedDate,ModifiedDate,CreatedUserId,ModifiedUserId,Id")] Module module)
        {
            if (ModelState.IsValid)
            {
                _context.Add(module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActualOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name", module.ActualOrderCardId);
            ViewData["CreatedUserId"] = new SelectList(_context.Users, "Id", "Id", module.CreatedUserId);
            ViewData["DestinationOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name", module.DestinationOrderCardId);
            ViewData["ModifiedUserId"] = new SelectList(_context.Users, "Id", "Id", module.ModifiedUserId);
            ViewData["ModuleTypeId"] = new SelectList(_context.ModulesTypes, "Id", "Code", module.ModuleTypeId);
            return View(module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules.FindAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            ViewData["ActualOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name", module.ActualOrderCardId);
            ViewData["CreatedUserId"] = new SelectList(_context.Users, "Id", "Id", module.CreatedUserId);
            ViewData["DestinationOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name", module.DestinationOrderCardId);
            ViewData["ModifiedUserId"] = new SelectList(_context.Users, "Id", "Id", module.ModifiedUserId);
            ViewData["ModuleTypeId"] = new SelectList(_context.ModulesTypes, "Id", "Code", module.ModuleTypeId);
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleTypeId,DestinationOrderCardId,SerialNumber,Place,IsInstalled,ActualOrderCardId,ManufacturingData,CreatedDate,ModifiedDate,CreatedUserId,ModifiedUserId,Id")] Module module)
        {
            if (id != module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.Id))
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
            ViewData["ActualOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name", module.ActualOrderCardId);
            ViewData["CreatedUserId"] = new SelectList(_context.Users, "Id", "Id", module.CreatedUserId);
            ViewData["DestinationOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name", @module.DestinationOrderCardId);
            ViewData["ModifiedUserId"] = new SelectList(_context.Users, "Id", "Id", module.ModifiedUserId);
            ViewData["ModuleTypeId"] = new SelectList(_context.ModulesTypes, "Id", "Code", module.ModuleTypeId);
            return View(module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules
                .Include(x => x.ActualOrderCard)
                .Include(x=> x.CreatedUser)
                .Include(x => x.DestinationOrderCard)
                .Include(x => x.ModifiedUser)
                .Include(x=> x.ModuleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @module = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }
    }
}
