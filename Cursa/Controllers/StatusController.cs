using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.StatusVM;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    [Authorize(Roles = "Менеджер")]
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
        public IActionResult Index() => View(new StatusDisplayViewModel());

        [HttpPost]
        public IActionResult GetStatus()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request
                    .Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchGlobalValue = Request.Form["search[value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                //var id = Request.Form["productId"].FirstOrDefault();
                //var productId = id != null ? Convert.ToInt32(id) : 0;

                var projectsData = _mapper.ProjectTo<StatusDisplayViewModel>(_context.Statuses
                    .AsNoTracking());

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchGlobalValue));
                }

                var recordsTotal = projectsData.Count();
                var data = projectsData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new
                    {draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data};
                return Ok(jsonData);
            }
            catch (Exception e)
            {
                _logger.LogError("Error search:{ExceptionMessage}", e.Message);
                return NotFound();
            }
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                // .Include(s => s.StatusType)
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
            // ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "Id", "StatusTypeName");
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Status status)
        {
            //IX_Statuses_Name
            if (ModelState.IsValid)
            {
                if (_context.Statuses.Any(x =>
                    String.Equals(x.Name, status.Name)))
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

            // ViewData["StatusTypeId"] =
            //     new SelectList(_context.StatusTypes, "Id", "StatusTypeName", status.StatusTypeId);
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

            // ViewData["StatusTypeId"] =
            //     new SelectList(_context.StatusTypes, "Id", "StatusTypeName", status.StatusTypeId);
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Status status)
        {
            if (id != status.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.Statuses.Any(x =>
                    String.Equals(x.Name, status.Name)
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

            // ViewData["StatusTypeId"] =
            //     new SelectList(_context.StatusTypes, "Id", "StatusTypeName", status.StatusTypeId);
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
                // .Include(s => s.StatusType)
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
            var status = await _context.Statuses
                // .Include(s => s.StatusType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (status == null)
            {
                return NotFound();
            }

            if (!status.IsSystem)
            {
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
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Невозможно удалить системный статус!");
            }

            return View(status);
        }

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }
    }
}