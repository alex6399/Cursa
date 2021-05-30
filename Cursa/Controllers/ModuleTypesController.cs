using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.ModuleTypesVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Entities;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    public class ModuleTypesController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ModuleTypesController> _logger;

        public ModuleTypesController(EfDbContext context, IMapper mapper, ILogger<ModuleTypesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: ModuleTypes
        public IActionResult Index() => View(new ModuleTypesDisplayViewModel());

        [HttpPost]
        public IActionResult GetModuleTypes()
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

                var projectsData = _mapper.ProjectTo<ModuleTypesDisplayViewModel>(_context.ModulesTypes
                    .AsNoTracking());

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchGlobalValue)
                                                           || m.Code.Contains(searchGlobalValue)
                                                           || m.NumberConnectionPoints.ToString()
                                                               .Contains(searchGlobalValue)
                                                           || m.CountChanel.ToString().Contains(searchGlobalValue));
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


        // GET: ModuleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleType = await _context.ModulesTypes
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
            return View();
        }

        // POST: ModuleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "Name,Code,CountChanel,IsActiv,IsCommunicationDevice,NumberConnectionPoints,Description,CreatedDate,Id")]
            ModuleType moduleType)
        {
            if (ModelState.IsValid)
            {
                if (moduleType.IsActiv && !moduleType.IsCommunicationDevice)
                {
                    var result = _context.ModulesTypes.Any(x => x.IsActiv && !x.IsCommunicationDevice);
                    if (result)
                    {
                        ModelState.AddModelError(string.Empty, "Уже существует действующий модуль LPBS");
                        return View(moduleType);
                    }
                }
                if (moduleType.IsCommunicationDevice)
                {
                    moduleType.NumberConnectionPoints = 0;
                }
                _context.Add(moduleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

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

            return View(moduleType);
        }

        // POST: ModuleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind(
                "Name,Code,CountChanel,IsActiv,IsCommunicationDevice,NumberConnectionPoints,Description,CreatedDate,Id")]
            ModuleType moduleType)
        {
            if (id != moduleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (moduleType.IsActiv && !moduleType.IsCommunicationDevice)
                {
                    var result = _context.ModulesTypes.Any(x => x.IsActiv 
                                                                && !x.IsCommunicationDevice
                                                                && x.Id!=moduleType.Id);
                    if (result)
                    {
                        ModelState.AddModelError(string.Empty, "Уже существует действующий модуль LPBS");
                        return View(moduleType);
                    }
                }
                try
                {
                    if (moduleType.IsCommunicationDevice)
                    {
                        moduleType.NumberConnectionPoints = 0;
                    }

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
            if (moduleType == null)
            {
                return NotFound();
            }

            try
            {
                _context.ModulesTypes.Remove(moduleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation("{ExceptionMessage}", e.Message);
                ModelState.AddModelError(String.Empty, "Невозможно удалить, тип модуля используется в системе");
            }

            return View(moduleType);
        }

        private bool ModuleTypeExists(int id)
        {
            return _context.ModulesTypes.Any(e => e.Id == id);
        }
    }
}