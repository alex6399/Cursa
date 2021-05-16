using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.SubProjectVM;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    [Authorize]
    public class SubProjectsController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SubProjectsController> _logger;

        public SubProjectsController(EfDbContext context, IMapper mapper, ILogger<SubProjectsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: SubProjects
        public async Task<IActionResult> Index()
        {
            var efDbContext = _context.SubProjects.AsNoTracking()
                .Include(s => s.Employee)
                .Include(s => s.Project)
                .Include(s => s.Status);
            return View(await efDbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetSubProject(int? projectId)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            var result =
                await _context.Projects.FirstOrDefaultAsync(p =>
                    p.Id == projectId); // TODO нужна ли эта проверка вообще
            if (result == null)
            {
                return NotFound();
            }

            return View(new SubProjectComplexDisplayViewModel
            {
                ProjectId = result.Id,
                ProjectName = result.Name
            });
        }

        // public async Task<IActionResult> GetSubProjectById(int? projectId) // TODO нужно переделать под Datatable
        // {
        //     if (projectId == null)
        //     {
        //         _logger.LogInformation("Страница не найдена");
        //         return NotFound();
        //     }
        //
        //     var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
        //     if (project == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var efDbContext = _context.SubProjects
        //         .Include(s => s.Employee)
        //         .Include(s => s.Project)
        //         .Include(s => s.Status)
        //         .Where(sp => sp.ProjectId == projectId);
        //     ViewData["ProjectId"] = project.Id; // TODO какие-то приколы с Nullable (int?) 
        //     // без проверки на null возвращается всегда null
        //     ViewData["ProjectName"] = project.Name;
        //     return View(await efDbContext.ToListAsync());
        // }


        [HttpPost]
        public IActionResult FindSubProjects()
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
                var searchCodeValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var searchEmployeeValue = Request.Form["columns[4][search][value]"].FirstOrDefault();
                var searchStatusValue = Request.Form["columns[5][search][value]"].FirstOrDefault();
                var searchContractValue = Request.Form["columns[6][search][value]"].FirstOrDefault();
                var searchDescriptionValue = Request.Form["columns[7][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var id = Request.Form["projectId"].FirstOrDefault();
                int projectId = id != null ? Convert.ToInt32(id) : 0;

                var projectsData = _mapper.ProjectTo<SubProjectsDisplayViewModel>(_context.SubProjects
                    .AsNoTracking()
                    .Where(subProject => subProject.ProjectId == projectId));

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchGlobalValue)
                                                           || m.Code.Contains(searchGlobalValue)
                                                           || m.Employee.FullName.Contains(searchGlobalValue)
                                                           || m.StatusName.Contains(searchGlobalValue)
                                                           || m.Contract.Name.Contains(searchGlobalValue)
                                                           || m.Description.Contains(searchGlobalValue));
                }

                if (!string.IsNullOrEmpty(searchNameValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchNameValue));
                }

                if (!string.IsNullOrEmpty(searchCodeValue))
                {
                    projectsData = projectsData.Where(m => m.Code.Contains(searchCodeValue));
                }

                if (!string.IsNullOrEmpty(searchEmployeeValue))
                {
                    projectsData = projectsData.Where(m => m.Employee.FullName.Contains(searchEmployeeValue));
                }

                if (!string.IsNullOrEmpty(searchStatusValue))
                {
                    projectsData = projectsData.Where(m => m.StatusName.Contains(searchStatusValue));
                }

                if (!string.IsNullOrEmpty(searchContractValue))
                {
                    projectsData = projectsData.Where(m => m.Contract.Name.Contains(searchContractValue));
                }

                if (!string.IsNullOrEmpty(searchDescriptionValue))
                {
                    projectsData = projectsData.Where(m => m.Description.Contains(searchDescriptionValue));
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

        // GET: SubProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects
                .Include(s => s.Employee)
                .Include(s => s.Project)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProject == null)
            {
                return NotFound();
            }

            return View(subProject);
        }

        // GET: SubProjects/Create/7
        public IActionResult Create(int? projectId)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return NotFound();
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "NameStatus");
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name");
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "Id", "Name");
            ViewBag.TitleProject = "для проекта: " + project.Name;
            return View(new SubProjectCreateEditViewModel
            {
                ProjectId = (int) projectId
            });
        }

        // POST: SubProjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,Name,Code,EmployeeId,StatusId,ContractId,ContractorId,Description")]
            SubProjectCreateEditViewModel subProjectDTO)
        {
            if (ModelState.IsValid)
            {
                if (_context.SubProjects.Any(x => x.Name == subProjectDTO.Name))
                {
                    ModelState.AddModelError("Name", "Подпроект уже существует");
                }

                if (_context.SubProjects.Any(x => x.Code == subProjectDTO.Code))
                {
                    ModelState.AddModelError("Code", "Код уже используется");
                }

                if (ModelState.IsValid)
                {
                    var subProject = _mapper.Map<SubProjectCreateEditViewModel, SubProject>(subProjectDTO);
                    _context.Add(subProject);
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(GetSubProject), new {projectId = subProjectDTO.ProjectId});
                    }
                    catch (DbUpdateException e)
                    {
                        var exception = e.InnerException;
                        if (exception != null && exception.Message.Contains("IX_SubProjects_Name"))
                        {
                            ModelState.AddModelError("Name", "Подпроект уже существует");
                        }

                        if (exception != null && exception.Message.Contains("IX_SubProjects_Code"))
                        {
                            ModelState.AddModelError("Code", "Такой код уже используется");
                        }
                    }
                }
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", subProjectDTO.EmployeeId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name", subProjectDTO.ContractId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "NameStatus", subProjectDTO.StatusId);
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "Id", "Name");
            return View(subProjectDTO);
        }

        // GET: SubProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects.FindAsync(id);
            var subProjectDTO = _mapper.Map<SubProject, SubProjectCreateEditViewModel>(subProject);
            if (subProject == null)
            {
                return NotFound();
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", subProject.EmployeeId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name", subProject.ContractId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "NameStatus", subProject.StatusId);
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "Id", "Name", subProject.ContractorId);
            return View(subProjectDTO);
        }

        // POST: SubProjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,ProjectId,Name,Code,EmployeeId,StatusId,ContractId,ContractorId,Description,CreatedDate")]
            SubProjectCreateEditViewModel subProjectDTO)
        {
            if (id != subProjectDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var subProject = _mapper.Map<SubProjectCreateEditViewModel, SubProject>(subProjectDTO);
                try
                {
                    _context.Update(subProject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetSubProject), new {projectId = subProjectDTO.ProjectId});
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubProjectExists(subProject.Id))
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
                    if (exception != null && exception.Message.Contains("IX_SubProjects_Name"))
                    {
                        ModelState.AddModelError("Name", "Подпроект уже существует");
                    }

                    if (exception != null && exception.Message.Contains("IX_SubProjects_Code"))
                    {
                        ModelState.AddModelError("Code", "Такой код уже используется");
                    }
                }
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FirstName", subProjectDTO.EmployeeId);
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "Id", "Name", subProjectDTO.ContractorId);
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Name", subProjectDTO.ContractId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "NameStatus", subProjectDTO.StatusId);
            return View(subProjectDTO);
        }

        // GET: SubProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subProject = await _context.SubProjects
                .Include(s => s.Employee)
                .Include(s => s.Project)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProject == null)
            {
                return NotFound();
            }

            return View(subProject);
        }

        // POST: SubProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subProject = await _context.SubProjects.FindAsync(id);
            try
            {
                _context.SubProjects.Remove(subProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation("{ExceptionMessage}", e.Message);
                ModelState.AddModelError(String.Empty, "Невозможно удалить, на данный подпроект имеются ссылки");
            }

            return View(subProject);
        }

        private bool SubProjectExists(int id)
        {
            return _context.SubProjects.Any(e => e.Id == id);
        }
    }
}