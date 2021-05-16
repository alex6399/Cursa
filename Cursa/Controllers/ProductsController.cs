using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.Base;
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

        [HttpGet]
        public IActionResult GetProductsForSubProject(int? subProjectId)
        {
            if (subProjectId == null)
            {
                return NotFound();
            }

            var subProject = _context.SubProjects.FirstOrDefault(p => p.Id == subProjectId);
            if (subProject == null)
            {
                return NotFound();
            }

            // var projectsData = _mapper.ProjectTo<ProductDisplayViewModel>(_context.Products
            //     .AsNoTracking());
            //.Where(subProject => subProject.ProjectId == projectId));
            return View(new ProductDisplayViewModel()
            {
                SubProject = new BaseViewModel()
                {
                    Id = subProject.Id,
                    Name = subProject.Name
                }
            });
        }
        // get products for a subproject

        [HttpPost]
        public IActionResult FindProductsForSubproject()
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
                var searchNameValue = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var searchSerialNumValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var searchCertifiedNumValue = Request.Form["columns[4][search][value]"].FirstOrDefault();
                // var searchStatusValue = Request.Form["columns[5][search][value]"].FirstOrDefault();
                // var searchContractValue = Request.Form["columns[6][search][value]"].FirstOrDefault();
                // var searchDescriptionValue = Request.Form["columns[7][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var id = Request.Form["subProjectId"].FirstOrDefault();
                var subProjectId = id != null ? Convert.ToInt32(id) : 0;

                var projectsData = _mapper.ProjectTo<ProductDisplayViewModel>(_context.Products
                    .AsNoTracking()
                    .Where(p => p.SubProjectId == subProjectId));

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchGlobalValue)
                                                           || m.SerialNum.Contains(searchGlobalValue)
                                                           || m.CertifiedNum.Contains(searchGlobalValue));
                }

                if (!string.IsNullOrEmpty(searchNameValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchNameValue));
                }

                if (!string.IsNullOrEmpty(searchSerialNumValue))
                {
                    projectsData = projectsData.Where(m => m.SerialNum.Contains(searchSerialNumValue));
                }

                if (!string.IsNullOrEmpty(searchCertifiedNumValue))
                {
                    projectsData = projectsData.Where(m => m.CertifiedNum.Contains(searchCertifiedNumValue));
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
        public IActionResult Create(int? subProjectId)
        {
            if (subProjectId == null)
            {
                return NotFound();
            }

            var subProject = _context.SubProjects.FirstOrDefault(p => p.Id == subProjectId);
            if (subProject == null)
            {
                return NotFound();
            }

            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "Name");
            ViewBag.TitleSubProject = "для подпроекта: " + subProject.Name;
            return View(new ProductCreateViewModel()
            {
                SubProjectId = (int) subProjectId
            });
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
                new SelectList(_context.SubProjects, "Id", "Name", productDto.SubProjectId);
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
            ViewData["SubProjectId"] = new SelectList(_context.SubProjects, "Id", "Name", product.SubProjectId);
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