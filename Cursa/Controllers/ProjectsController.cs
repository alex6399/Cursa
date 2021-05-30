using System;
using System.Collections.Generic;
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

        public IActionResult Index() => View(new ProjectViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetProject()
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
                var searchNameValue = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var searchCodeValue = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var searchOwnerValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var searchEmployeeValue = Request.Form["columns[4][search][value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                // var projectsData = _context.Projects.Select(x => new ProjectViewModel
                // {
                //     Id = x.Id,
                //     Name = x.Name,
                //     Code = x.Code,
                //     OwnerName = x.Owner.Name,
                //     EmployeeFullName = x.Employee.LastName
                // });
                var projectsData = _mapper.ProjectTo<ProjectViewModel>(_context.Projects);

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchGlobalValue)
                                                           || m.Code.Contains(searchGlobalValue)
                                                           || m.Owner.Contains(searchGlobalValue)
                                                           || m.Employee.Contains(searchGlobalValue));
                }

                if (!string.IsNullOrEmpty(searchNameValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchNameValue));
                }

                if (!string.IsNullOrEmpty(searchCodeValue))
                {
                    projectsData = projectsData.Where(m => m.Code.Contains(searchCodeValue));
                }

                if (!string.IsNullOrEmpty(searchOwnerValue))
                {
                    projectsData = projectsData.Where(m => m.Owner.Contains(searchOwnerValue));
                }

                if (!string.IsNullOrEmpty(searchEmployeeValue))
                {
                    projectsData = projectsData.Where(m => m.Employee.Contains(searchEmployeeValue));
                }

                var recordsTotal = 0;
                recordsTotal = projectsData.Count();
                var data = projectsData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new
                    {draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data};
                return Ok(jsonData);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Employee)
                .Include(p => p.Owner)
                .Include(p => p.ModifiedUser)
                .Include(p => p.CreatedUser)
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees
                .Where(x => x.Department.IsResponsibleProjectsAndSubProjects), "Id", "GetFullName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Name");
            return View(new ProjectCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OwnerId,Code,EmployeeId,Description")]
            ProjectCreateViewModel projectVM)
        {
            if (ModelState.IsValid)
            {
                var project = _mapper.Map<ProjectCreateViewModel, Project>(projectVM);
                _context.Add(project);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException e)
                {
                    var exception = e.InnerException;
                    if (exception != null && exception.Message.Contains("IX_Projects_Name"))
                    {
                        ModelState.AddModelError("Name", "Проект уже существует");
                    }

                    if (exception != null && exception.Message.Contains("IX_Projects_Code"))
                    {
                        ModelState.AddModelError("Code", "Такой код уже используется");
                    }
                }
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees
                    .Where(x => x.Department.IsResponsibleProjectsAndSubProjects),
                "Id", "GetFullName");
            ViewData["OwnerId"] = new SelectList(_context.Owners,
                "Id", "Name",
                projectVM.OwnerId);
            return View(projectVM);
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

            var projectDTO = _mapper.Map<Project, ProjectEditViewModel>(project);
            ViewData["EmployeeId"] = new SelectList(_context.Employees
                .Where(x => x.Department.IsResponsibleProjectsAndSubProjects), "Id", "GetFullName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Name", project.OwnerId);
            return View(projectDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name,OwnerId,Code,EmployeeId,Description")]
            ProjectEditViewModel projectDTO)
        {
            if (id != projectDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var project = _mapper.Map<ProjectEditViewModel, Project>(projectDTO);
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
                    if (exception != null && exception.Message.Contains("IX_Projects_Name"))
                    {
                        ModelState.AddModelError("Name", "Проект уже существует");
                    }

                    if (exception != null && exception.Message.Contains("IX_Projects_Code"))
                    {
                        ModelState.AddModelError("Code", "Такой код уже используется");
                    }
                }
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees
                .Where(x => x.Department.IsResponsibleProjectsAndSubProjects), "Id", "GetFullName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Name", projectDTO.OwnerId);
            return View(projectDTO);
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
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation("{ExceptionMessage}", e.Message);
                ModelState.AddModelError(String.Empty, "Невозможно удалить, данный проект задействован");
            }

            return View(project);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        [HttpGet]
        public JsonResult IsCodeProjectExist(string Code, int? Id)
        {
            if (Id == null)
            {
                return Json(!_context.Projects.Any(x => x.Code == Code));
            }
            else
            {
                return Json(!_context.Projects.Any(x => x.Code == Code && x.Id != Id));
            }
        }

        [HttpGet]
        public JsonResult IsNameProjectExist(string Name, int? Id)
        {
            if (Id == null)
            {
                return Json(!_context.Projects.Any(x => x.Name == Name));
            }
            else
            {
                return Json(!_context.Projects.Any(x => x.Name == Name && x.Id != Id));
            }
        }

        public IActionResult GetProjects()
        {
            var projects = _context.Projects.AsNoTracking()
                .OrderBy(n => n.CreatedDate)
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name + "(" + x.Code + ")"
                    }).ToList();
            var projectStartEmpty = new SelectListItem()
            {
                Value = null,
                Text = "Выберите проект"
            };
            projects.Insert(0, projectStartEmpty);
            return Json(new SelectList(projects, "Value", "Text"));
        }
    }
}