using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.EmployeesVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Entities;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(EfDbContext context, IMapper mapper, ILogger<EmployeesController> logger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var efDbContext = _mapper.ProjectTo<EmployeesViewModel>(_context.Employees);
            _logger.LogInformation("View Employee List");
            return View(await efDbContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult GetEmployee()
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
                var searchFullNameValue = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var searchPhoneValue = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var searchDepartmentValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;

                var projectsData = _mapper.ProjectTo<EmployeesViewModel>(_context.Employees);

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.FullName.Contains(searchGlobalValue)
                                                           || m.Phone.Contains(searchGlobalValue)
                                                           || m.DepartmentName.Contains(searchGlobalValue));
                }

                if (!string.IsNullOrEmpty(searchFullNameValue))
                {
                    projectsData = projectsData.Where(m => m.FullName.Contains(searchFullNameValue));
                }

                if (!string.IsNullOrEmpty(searchPhoneValue))
                {
                    projectsData = projectsData.Where(m => m.Phone.Contains(searchPhoneValue));
                }
                if (!string.IsNullOrEmpty(searchDepartmentValue))    
                {
                    projectsData = projectsData.Where(m => m.DepartmentName.Contains(searchDepartmentValue));
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

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.CreatedUser)
                .Include(e => e.Department)
                .Include(e => e.ModifiedUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View(new EmployeeCreateEditViewModel());
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,MiddleName,LastName,Phone,DepartmentId,Id")]
            EmployeeCreateEditViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var employeeIns = _mapper.Map<EmployeeCreateEditViewModel, Employee>(employee);
                try
                {
                    _context.Add(employeeIns);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    _logger.LogError("{ExceptionMessage}",dbUpdateException.Message);
                    ModelState.AddModelError(String.Empty, "Ошибка при сохранении");
                    //return View(employee);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.Message);
                }
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            var employeeIns = _mapper.Map<Employee, EmployeeCreateEditViewModel>(employee);
            if (employeeIns == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employeeIns);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,MiddleName,LastName,Phone,DepartmentId,Id")]
            EmployeeCreateEditViewModel employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var employeeIns = _mapper.Map<EmployeeCreateEditViewModel, Employee>(employee);
                try
                {
                    _context.Update(employeeIns);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.CreatedUser)
                .Include(e => e.Department)
                .Include(e => e.ModifiedUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}