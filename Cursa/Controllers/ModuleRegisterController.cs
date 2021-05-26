using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Cursa.ViewModels.ModuleRegisterVM;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    public class ModuleRegisterController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ModuleRegisterController> _logger;

        public ModuleRegisterController(EfDbContext context, IMapper mapper, ILogger<ModuleRegisterController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET
        public IActionResult GetModuleRegister() => View();

        [HttpPost]
        public IActionResult FindModuleRegister()
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
                var searchModuleNameValue = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var searchSerialValue = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var searchNumberDestValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var searchNumberActValue = Request.Form["columns[4][search][value]"].FirstOrDefault();
                var searchProductNameValue = Request.Form["columns[5][search][value]"].FirstOrDefault();
                var searchProductNumberValue = Request.Form["columns[6][search][value]"].FirstOrDefault();
                var searchSubProjectNameValue = Request.Form["columns[7][search][value]"].FirstOrDefault();
                // var searchContractValue = Request.Form["columns[8][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                // var id = Request.Form["projectId"].FirstOrDefault();
                // int projectId = id != null ? Convert.ToInt32(id) : 0;
                // var dataDebug =  _context.Modules
                //         .Include(x => x.ModuleType)
                //         .Include(x => x.DestinationOrderCard)
                //         .Include(x => x.ActualOrderCard)
                //         .ThenInclude(x => x.Product)
                //         .ThenInclude(x => x.SubProject)
                //         .Where(x=>x.ActualOrderCardId!=null)
                //     ;

                var projectsData = _mapper.ProjectTo<ModuleRegisterViewModel>(_context.Modules
                    .Include(x => x.ModuleType)
                    .Include(x => x.DestinationOrderCard)
                    .Include(x => x.ActualOrderCard)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.SubProject)
                    .Where(x => x.ActualOrderCardId != null)
                    .AsNoTracking());

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.SubProjectName.Contains(searchGlobalValue));
                }

                if (!string.IsNullOrEmpty(searchModuleNameValue))
                {
                    projectsData = projectsData.Where(m => m.ModuleTypeName.Contains(searchModuleNameValue));
                }

                if (!string.IsNullOrEmpty(searchNumberDestValue))
                {
                    projectsData = projectsData.Where(m => m.DestOrderCardNumber.Contains(searchNumberDestValue));
                }

                if (!string.IsNullOrEmpty(searchSerialValue))
                {
                    projectsData =
                        projectsData.Where(m => m.SerialNumber != null && m.SerialNumber.Contains(searchSerialValue));
                }

                if (!string.IsNullOrEmpty(searchNumberActValue))
                {
                    projectsData = projectsData.Where(m => m.DestOrderCardNumber.Contains(searchNumberActValue));
                }

                if (!string.IsNullOrEmpty(searchProductNumberValue))
                {
                    projectsData = projectsData.Where(m => m.ProductNumber.Contains(searchProductNumberValue));
                }

                if (!string.IsNullOrEmpty(searchProductNameValue))
                {
                    projectsData = projectsData.Where(m => m.ProductName.Contains(searchProductNameValue));
                }
                if (!string.IsNullOrEmpty(searchSubProjectNameValue))
                {
                    projectsData = projectsData.Where(m => m.SubProjectName.Contains(searchSubProjectNameValue));
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