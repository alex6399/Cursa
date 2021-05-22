using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.DepartmentVM;
using Cursa.ViewModels.ProductTypesVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Entities;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductTypesController> _logger;

        public ProductTypesController(EfDbContext context, IMapper mapper, ILogger<ProductTypesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: ProductTypes
        public IActionResult Index()
            => View(new ProductTypesDisplayViewModel());

        [HttpPost]
        public IActionResult GetProductTypes()
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

                var projectsData = _mapper.ProjectTo<ProductTypesDisplayViewModel>(_context.ProductTypes
                    .AsNoTracking());

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchGlobalValue)
                                                           || m.ProductTypeName.Contains(searchGlobalValue));
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

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .Include(p => p.ProductSubType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            ViewData["ProductSubTypeId"] = new SelectList(_context.ProductSubTypes, "Id", "Name");
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ProductSubTypeId,Description,Id")]
            ProductType productType)
        {
            if (ModelState.IsValid)
            {
                if (_context.ProductTypes.Any(x =>
                    String.Equals(x.Name, productType.Name)))
                {
                    ModelState.AddModelError("Name", "Такой тип уже существует");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(productType);
                    try
                    {
                        _context.Add(productType);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException e)
                    {
                        var exception = e.InnerException;
                        if (exception != null && exception.Message.Contains("IX_ProductTypes_Name"))
                        {
                            ModelState.AddModelError("Name", "Тип уже существует");
                        }
                    }
                }
            }

            ViewData["ProductSubTypeId"] =
                new SelectList(_context.ProductSubTypes, "Id", "Name", productType.ProductSubTypeId);
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            ViewData["ProductSubTypeId"] =
                new SelectList(_context.ProductSubTypes, "Id", "Name", productType.ProductSubTypeId);
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ProductSubTypeId,Description,Id")]
            ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.ProductTypes.Any(x =>
                    String.Equals(x.Name, productType.Name) && x.Id != productType.Id))
                {
                    ModelState.AddModelError("Name", "Такой тип уже существует");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(productType);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductTypeExists(productType.Id))
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
                        if (exception != null && exception.Message.Contains("IX_ProductTypes_Name"))
                        {
                            ModelState.AddModelError("Name", "Тип уже существует");
                        }
                    }
                }
            }

            ViewData["ProductSubTypeId"] =
                new SelectList(_context.ProductSubTypes, "Id", "Name", productType.ProductSubTypeId);
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .Include(p => p.ProductSubType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            try
            {
                _context.ProductTypes.Remove(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation("{ExceptionMessage}", e.Message);
                ModelState.AddModelError(String.Empty, "Невозможно удалить, данный тип продукта задействован!");
            }

            return View(productType);
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.Id == id);
        }
    }
}