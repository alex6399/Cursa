using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.ProjectVM;
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
    public class ProjectsController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(EfDbContext context, IMapper mapper, ILogger<ProjectsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projects = _context.Projects.AsNoTracking().Select(p => new ProjectViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Owner = p.Owner.Name,
                Code = p.Code,
                Employee = p.Employee.GetFullName
            });
            return View(await projects.ToListAsync());
        }

        [HttpPost]
        public IActionResult GetProject()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                var projectsData = _context.Projects.Select(x => new ProjectViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Owner = x.Owner.Name,
                    Employee = x.Employee.LastName
                });

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchValue)
                                                           || m.Code.Contains(searchValue)
                                                           || m.Employee.Contains(searchValue)
                                                           || m.Owner.Contains(searchValue));
                }
                int recordsTotal = 0;
                recordsTotal = projectsData.Count();
                var data = projectsData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Employee)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Name");
            return View(new ProjectCreateViewModel());
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OwnerId,Code,EmployeeId,Description")] ProjectCreateViewModel project)
        {
            
            if (ModelState.IsValid)
            {
                if (_context.Projects.Any(x => x.Code == project.Code))
                {
                    ModelState.AddModelError("Code",
                        "Данный код уже используется");
                }
                
                _context.Add(new Project
                {
                    Id = project.Id,
                    Name = project.Name,
                    OwnerId = project.OwnerId,
                    Code = project.Code,
                    EmployeeId = project.EmployeeId,
                    Description = project.Description
                    
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", project.EmployeeId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Name", project.OwnerId);
            return View(project);
        }
        [HttpGet]
        public JsonResult IsCodeProjectExist(string code)
        {
            return Json(!_context.Projects.Any(x => x.Code == code));
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);


            if (project == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", project.EmployeeId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Name", project.OwnerId);
            return View(new ProjectEditViewModel
            {
                Id = project.Id,
                Name = project.Name,
                OwnerId = project.OwnerId,
                Code = project.Code,
                EmployeeId = project.EmployeeId,
                Description = project.Description

            });
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OwnerId,Code,EmployeeId,Description,CreatedDate")] ProjectEditViewModel project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }
            
            var projectDuplicateByCode = _context.Projects.FirstOrDefault(
                x => x.Id == id && x.Code.Equals(project.Code));

            if (projectDuplicateByCode == null)
            {
                ModelState.AddModelError(key: "Code", errorMessage: "Данный код уже используется");
            }

            if (ModelState.IsValid)
            {
                projectDuplicateByCode.Name = project.Name;
                projectDuplicateByCode.EmployeeId = project.EmployeeId;
                projectDuplicateByCode.OwnerId = project.OwnerId;
                projectDuplicateByCode.Code = project.Code;
                projectDuplicateByCode.Description = project.Description;
                try
                {
                    _context.Update(projectDuplicateByCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(projectDuplicateByCode.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", project.EmployeeId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Name", project.OwnerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Employee)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
