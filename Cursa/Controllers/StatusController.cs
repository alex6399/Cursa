using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    [Authorize]
    public class StatusController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<StatusController> _logger;

        public StatusController(EfDbContext context, IMapper mapper, ILogger<StatusController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
            var efDbContext = _context.Statuses.Include(s => s.StatusType);
            return View(await efDbContext.ToListAsync());
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                .Include(s => s.StatusType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "Id", "StatusTypeName");
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameStatus,StatusTypeId")] Status status)
        {
            //IX_Statuses_Name
            if (ModelState.IsValid)
            {
                if (_context.Statuses.Any(x =>
                    String.Equals(x.Name, status.Name, StringComparison.CurrentCultureIgnoreCase)
                    && x.StatusTypeId == status.StatusTypeId))
                {
                    ModelState.AddModelError("Name", "Такой статус уже существует");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(status);
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException e)
                    {
                        var exception = e.InnerException;
                        if (exception != null && exception.Message.Contains("IX_Statuses_Name"))
                        {
                            ModelState.AddModelError("Name", "Статус уже существует");
                        }
                    }
                }
            }

            ViewData["StatusTypeId"] =
                new SelectList(_context.StatusTypes, "Id", "StatusTypeName", status.StatusTypeId);
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            ViewData["StatusTypeId"] =
                new SelectList(_context.StatusTypes, "Id", "StatusTypeName", status.StatusTypeId);
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameStatus,StatusTypeId")] Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.Statuses.Any(x =>
                    String.Equals(x.Name, status.Name, StringComparison.CurrentCultureIgnoreCase)
                    && x.StatusTypeId == status.StatusTypeId
                    && x.Id != status.Id))
                {
                    ModelState.AddModelError("Name", "Такой статус уже существует");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(status);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StatusExists(status.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    catch (DbUpdateException e)
                    {
                        var exception = e.InnerException;
                        if (exception != null && exception.Message.Contains("IX_Statuses_Name"))
                        {
                            ModelState.AddModelError("Name", "Статус уже существует");
                        }
                    }
                }
            }

            ViewData["StatusTypeId"] =
                new SelectList(_context.StatusTypes, "Id", "StatusTypeName", status.StatusTypeId);
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                .Include(s => s.StatusType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            _context.Statuses.Remove(status);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation("{ExceptionMessage}", e.Message);
                ModelState.AddModelError(String.Empty, "Невозможно удалить, данный статус задействован!");
            }

            return View(status);
        }

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }
    }
}