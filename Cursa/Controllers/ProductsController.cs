using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.EmployeesVM;
using Cursa.ViewModels.ProductsVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(EfDbContext context, IMapper mapper, ILogger<ProductsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Products
        public IActionResult Index()
        {
            var projectsData = _mapper.ProjectTo<ProductDisplayViewModel>(_context.Products
                .AsNoTracking());
            //.Where(subProject => subProject.ProjectId == projectId));
            return View(projectsData);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CreatedUser)
                .Include(p => p.ModifiedUser)
                .Include(p => p.ProductType)
                .Include(p => p.SubProject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name");
            ViewData["SubProjectId"] = new SelectList(_context.SubProjects, "Id", "Code");
            return View(new ProductCreateViewModel());
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "Name,SerialNum,CertifiedNum,ProductTypeId,SubProjectId,IsFormed,ManufacturingDate,ShippedDate,Id,Description")]
            ProductCreateViewModel productDto)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<ProductCreateViewModel, Product>(productDto);
                _context.Add(product);
                try
                {
                    await _context.SaveChangesAsync(); // TODO нужна проверка на уникальность серийного номера...
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException e)
                {
                    var exception = e.InnerException;
                    if (exception != null && exception.Message.Contains("IX_Products_SerialNum"))
                    {
                        ModelState.AddModelError("SerialNum", "Такой номер уже используется");
                    }

                    // TODO причем это не выполнится за 1 операцию с предыдущим if
                    if (exception != null && exception.Message.Contains("IX_Products_CertifiedNum"))
                    {
                        ModelState.AddModelError("CertifiedNum", "Такой номер уже используется");
                    }

                    /*
                    ViewData["ProductTypeId"] =
                        new SelectList(_context.ProductTypes, "Id", "Name", productDto.ProductTypeId);
                    ViewData["SubProjectId"] =
                        new SelectList(_context.SubProjects, "Id", "Code", productDto.SubProjectId);
                    // TODO как лучше селекты во view передавать, так или через ViewModel
                    return View(productDto); // TODO как исправить это дублирование
                    */
                }
            }

            ViewData["ProductTypeId"] =
                new SelectList(_context.ProductTypes, "Id", "Name", productDto.ProductTypeId);
            ViewData["SubProjectId"] =
                new SelectList(_context.SubProjects, "Id", "Code", productDto.SubProjectId);
            return View(productDto);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductTypeId);
            ViewData["SubProjectId"] = new SelectList(_context.SubProjects, "Id", "Code", product.SubProjectId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Name,SerialNum,CertifiedNum,ProductTypeId,SubProjectId,IsFormed,ManufacturingDate,Id,Description")]
            Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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

            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name", product.ProductTypeId);
            ViewData["SubProjectId"] = new SelectList(_context.SubProjects, "Id", "Code", product.SubProjectId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CreatedUser)
                .Include(p => p.ModifiedUser)
                .Include(p => p.ProductType)
                .Include(p => p.SubProject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}