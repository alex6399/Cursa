using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Cursa.ViewModels.ProjectRegisterVM;
using Cursa.ViewModels.SubProjectVM;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    [Authorize(Roles = "Менеджер")]
    public class ProjectRegisterController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectRegisterController> _logger;

        public ProjectRegisterController(EfDbContext context, IMapper mapper, ILogger<ProjectRegisterController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET
        public IActionResult GetProjectRegister() => View(new SubProjectComplexDisplayViewModel());

        [HttpPost]
        public IActionResult FindProjectRegister()
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
                var searchProjectOwnerValue = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var searchProjectNameValue = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var searchNameValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var searchCodeValue = Request.Form["columns[4][search][value]"].FirstOrDefault();
                var searchEmployeeValue = Request.Form["columns[5][search][value]"].FirstOrDefault();
                var searchStatusValue = Request.Form["columns[6][search][value]"].FirstOrDefault();
                var searchContractorValue = Request.Form["columns[7][search][value]"].FirstOrDefault();
                var searchContractValue = Request.Form["columns[8][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                // var id = Request.Form["projectId"].FirstOrDefault();
                // int projectId = id != null ? Convert.ToInt32(id) : 0;

                var projectsData = _mapper.ProjectTo<ProjectRegisterDisplayViewModel>(_context.SubProjects
                    .Include(x => x.Project)
                    .AsNoTracking());

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.Owner.Contains(searchGlobalValue)
                                                           || m.ProjectName.Contains(searchGlobalValue)
                                                           || m.Name.Contains(searchGlobalValue)
                                                           || m.Code.Contains(searchGlobalValue)
                                                           || m.Employee.FullName.Contains(searchGlobalValue)
                                                           || m.StatusName.Contains(searchGlobalValue)
                                                           || m.Contractor.Contains(searchGlobalValue)
                                                           || m.Contract.Contains(searchGlobalValue));
                }

                if (!string.IsNullOrEmpty(searchProjectOwnerValue))
                {
                    projectsData = projectsData.Where(m => m.Owner.Contains(searchProjectOwnerValue));
                }

                if (!string.IsNullOrEmpty(searchProjectNameValue))
                {
                    projectsData = projectsData.Where(m => m.ProjectName.Contains(searchProjectNameValue));
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

                if (!string.IsNullOrEmpty(searchContractorValue))
                {
                    projectsData = projectsData.Where(m => m.Contractor.Contains(searchContractorValue));
                }

                if (!string.IsNullOrEmpty(searchContractValue))
                {
                    projectsData = projectsData.Where(m => m.Contract.Contains(searchContractValue));
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
    }
}