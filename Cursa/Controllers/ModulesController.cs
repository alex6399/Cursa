using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.ModuleVM;
using Cursa.ViewModels.OrderCardVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;

namespace Cursa.Controllers
{
    [Authorize(Roles = "Менеджер")]
    public class ModulesController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ModulesController> _logger;

        public ModulesController(EfDbContext context, IMapper mapper, ILogger<ModulesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // // GET: Modules
        // public async Task<IActionResult> Index()
        // {
        //     var efDbContext = _context.Modules
        //         .Include(x => x.ActualOrderCard)
        //         // .Include(x => x.CreatedUser)
        //         // // .Include(x => x.ModifiedUser)
        //         .Include(x => x.DestinationOrderCard)
        //         .Include(x => x.ModuleType);
        //     return View(await efDbContext.ToListAsync());
        // }

        [HttpGet]
        public async Task<IActionResult> GetModulesForCardOrder(int cardOrderId)
        {
            var cardOrder = await _context.OrderCards.FirstOrDefaultAsync(p => p.Id == cardOrderId);
            if (cardOrder == null)
            {
                return NotFound();
            }

            return View(new ModuleDisplayViewModel()
            {
                ProductId = cardOrder.ProductId,
                DestinationOrderCardId = cardOrderId,
                DestinationOrderCardName = cardOrder?.Name,
                DestinationOrderCardNumber = cardOrder?.Number
            });
        }

        [HttpPost]
        public IActionResult FindModulesForCardOrder()
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
                // var searchNameValue = Request.Form["columns[2][search][value]"].FirstOrDefault();
                // var searchCodeValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                // var searchEmployeeValue = Request.Form["columns[4][search][value]"].FirstOrDefault();
                // var searchStatusValue = Request.Form["columns[5][search][value]"].FirstOrDefault();
                // var searchContractValue = Request.Form["columns[6][search][value]"].FirstOrDefault();
                // var searchDescriptionValue = Request.Form["columns[7][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var id = Request.Form["orderId"].FirstOrDefault();
                int orderId = id != null ? Convert.ToInt32(id) : 0;

                var projectsData = _mapper.ProjectTo<ModuleDisplayViewModel>(_context.Modules
                    .AsNoTracking()
                    .Where(x => x.DestinationOrderCardId == orderId));

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.SerialNumber.Contains(searchGlobalValue)
                        // || m.Code.Contains(searchGlobalValue)
                        // || m.Employee.FullName.Contains(searchGlobalValue)
                        // || m.StatusName.Contains(searchGlobalValue)
                        // || m.Contract.Contains(searchGlobalValue)
                        // || m.Description.Contains(searchGlobalValue)
                    );
                }

                // if (!string.IsNullOrEmpty(searchNameValue))
                // {
                //     projectsData = projectsData.Where(m => m.Name.Contains(searchNameValue));
                // }
                //
                // if (!string.IsNullOrEmpty(searchCodeValue))
                // {
                //     projectsData = projectsData.Where(m => m.Code.Contains(searchCodeValue));
                // }
                //
                // if (!string.IsNullOrEmpty(searchEmployeeValue))
                // {
                //     projectsData = projectsData.Where(m => m.Employee.FullName.Contains(searchEmployeeValue));
                // }
                //
                // if (!string.IsNullOrEmpty(searchStatusValue))
                // {
                //     projectsData = projectsData.Where(m => m.StatusName.Contains(searchStatusValue));
                // }
                //
                // if (!string.IsNullOrEmpty(searchContractValue))
                // {
                //     projectsData = projectsData.Where(m => m.Contract.Contains(searchContractValue));
                // }
                //
                // if (!string.IsNullOrEmpty(searchDescriptionValue))
                // {
                //     projectsData = projectsData.Where(m => m.Description.Contains(searchDescriptionValue));
                // }

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

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(x => x.ActualOrderCard)
                .Include(p => p.ModifiedUser)
                .Include(p => p.CreatedUser)
                .Include(p => p.Employee)
                .Include(x => x.DestinationOrderCard)
                .Include(x => x.ModuleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // // GET: Modules/Create
        // public async Task<IActionResult> Create(int cardOrderId)
        // {
        //     var cardOrder = await _context.OrderCards.FirstOrDefaultAsync(x => x.Id == cardOrderId);
        //     if (cardOrder == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var addressesModules = cardOrder.AddressesModule;
        //     var modules = _context.OrderCardItems
        //         .Where(x => x.OrderCardId == cardOrderId)
        //         .Include(x => x.ModuleType)
        //         .Select(x => new ModuleCreateEditViewModel()
        //         {
        //             ModuleTypeId = x.ModuleTypeId,
        //             ModuleTypeName = x.ModuleType.Name,
        //             ActualOrderCard = new OrderCardBaseViewModel()
        //             {
        //                 Id = x.OrderCardId,
        //                 Name = x.OrderCard.Name,
        //                 Number = x.OrderCard.Number
        //             }
        //         });
        //     foreach (var moduleCreateEditViewModel in modules)
        //     {
        //         moduleCreateEditViewModel.Place = 
        //             addressesModules[moduleCreateEditViewModel.ModuleTypeId];
        //     }
        //     // var modules = new List<ModuleCreateEditViewModel>();
        //     // var addressesModules = cardOrder.AddressesModule;
        //     // foreach (var kvp in addressesModules)
        //     // {
        //     //     modules.Add(new ModuleCreateEditViewModel()
        //     //     {
        //     //         ModuleTypeId = kvp.Value,
        //     //         Place = kvp.Key
        //     //     });
        //     // }
        //     ViewData["EmployeeId"] = new SelectList(_context.Employees
        //         .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");
        //     ViewData["ActualOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name");
        //     ViewData["DestinationOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name");
        //     ViewData["ModuleTypeId"] = new SelectList(_context.ModulesTypes, "Id", "Code");
        //     // ViewData["CreatedUserId"] = new SelectList(_context.Users, "Id", "Id");
        //     // ViewData["ModifiedUserId"] = new SelectList(_context.Users, "Id", "Id");
        //     return View();
        // }
        //
        // // POST: Modules/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create(
        //     [Bind(
        //         "ModuleTypeId,DestinationOrderCardId,SerialNumber,Place,IsInstalled,ActualOrderCardId,ManufacturingData,Id,EmployeeId")]
        //     Module module)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(module);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(GetModulesForCardOrder), new {cardOrderId=module.DestinationOrderCardId});
        //     }
        //     ViewData["EmployeeId"] = new SelectList(_context.Employees
        //         .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");
        //     ViewData["ActualOrderCardId"] = new SelectList(_context.OrderCards, "Id", "Name", module.ActualOrderCardId);
        //     ViewData["DestinationOrderCardId"] =
        //         new SelectList(_context.OrderCards, "Id", "Name", module.DestinationOrderCardId);
        //     ViewData["ModuleTypeId"] = new SelectList(_context.ModulesTypes, "Id", "Code", module.ModuleTypeId);
        //     return View(module);
        // }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules
                .Include(x => x.ModuleType)
                .Include(x => x.DestinationOrderCard)
                .Include(x => x.ActualOrderCard)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.SubProject)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (module == null)
            {
                return NotFound();
            }

            if (module.ActualOrderCard != null)
            {
                var projectId = module.ActualOrderCard.Product.SubProject.ProjectId;
                // Start: project
                ViewData["ProjectId"] = GetProject(projectId);
                // End: project

                // Start: subProject
                var subProjectId = module.ActualOrderCard.Product.SubProjectId;
                ViewData["SubProjectId"] = GetSubProject(projectId);
                // End: SubProject

                // Start: Product
                var productId = module.ActualOrderCard.ProductId;
                ViewData["ProductId"] = GetProducts(subProjectId);
                // End: Product

                // Start: CardOrder
                var cardOrderId = module.ActualOrderCardId.Value;
                ViewData["CardOrderId"] = GetOrderCards(productId);
                // End: CardOrder

                // Start: Module
                ViewData["ModuleId"] = GetModules(cardOrderId,module.ModuleTypeId,module.ActualPlace );
            }else{ ViewData["ProjectId"] = GetProject();}

           
            var moduleVm = _mapper.Map<Module, ModuleCreateEditViewModel>(module);
            ViewData["EmployeeId"] = new SelectList(_context.Employees
                .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");
           return View(moduleVm);
        }

        // POST: Modules/Edit/5

        // To protect from overposting attacks, enable the specific properties you want to bind to.

        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ModuleCreateEditViewModel moduleVm)
        {
            if (id != moduleVm.Id)
            {
                return NotFound();
            }
            var moduleDb = await _context.Modules
                .AsNoTracking()
                .Include(x => x.ModuleType)
                .Include(x => x.DestinationOrderCard)
                .Include(x => x.ActualOrderCard)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.SubProject)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (ModelState.IsValid)
            {
                var module = _mapper.Map<ModuleCreateEditViewModel, Module>(moduleVm);

                try
                {
                    _context.Update(module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.Id))
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
                    if (exception != null && exception.Message.Contains("IX_Modules_SerialNumber"))
                    {
                        ModelState.AddModelError("SerialNumber", "Такой номер уже используется");
                    }
                }

                return RedirectToAction(nameof(GetModulesForCardOrder),
                    new {cardOrderId = moduleVm.DestinationOrderCardId});
            }

            moduleVm.ModuleTypeName = moduleDb.ModuleType.Name;
            moduleVm.DestinationOrderCardName = moduleDb.DestinationOrderCard.Name;
            moduleVm.DestinationOrderCardNumber = moduleDb.DestinationOrderCard.Number;
            if (moduleDb.ActualOrderCard != null)
            {
                var projectId = moduleDb.ActualOrderCard.Product.SubProject.ProjectId;
                // Start: project
                ViewData["ProjectId"] = GetProject(projectId);
                // End: project

                // Start: subProject
                var subProjectId = moduleDb.ActualOrderCard.Product.SubProjectId;
                ViewData["SubProjectId"] = GetSubProject(projectId);
                // End: SubProject

                // Start: Product
                var productId = moduleDb.ActualOrderCard.ProductId;
                ViewData["ProductId"] = GetProducts(subProjectId);
                // End: Product

                // Start: CardOrder
                var cardOrderId = moduleDb.ActualOrderCardId.Value;
                ViewData["CardOrderId"] = GetOrderCards(productId);
                // End: CardOrder

                // Start: Module
                ViewData["ModuleId"] = GetModules(cardOrderId,moduleDb.ModuleTypeId,moduleDb.ActualPlace );
            }else{ ViewData["ProjectId"] = GetProject();}

            ViewData["EmployeeId"] = new SelectList(_context.Employees
                .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");
            
            return View(moduleVm);
        }

        // GET: Modules/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules
                .Include(x => x.ActualOrderCard)
                .Include(x => x.DestinationOrderCard)
                .Include(x => x.ModuleType)
                .Include(x => x.CreatedUser)
                .Include(x => x.ModifiedUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (module == null)
            {
                return NotFound();
            }

            return View(module);
        }

        // POST: Modules/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @module = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetModulesForCardOrder), new {cardOrderId = module.DestinationOrderCardId});
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }
        
        [HttpGet]
        public JsonResult IsSerialNumberModuleExist(string SerialNumber, int? Id)
        {
            if (Id == null)
            {
                return Json(!_context.Modules.Any(x => x.SerialNumber == SerialNumber));
            }
            else
            {
                return Json(!_context.Modules.Any(x => x.SerialNumber == SerialNumber
                                                       && x.Id != Id));
            }
        }

        public IActionResult GetModules(int destinationOrderCardId)
        {
            var modules = _context.Modules.AsNoTracking()
                .OrderBy(n => n.CreatedDate)
                .Where(x => x.DestinationOrderCardId == destinationOrderCardId && x.ActualOrderCardId == null)
                .Select(x =>
                    new SelectListItem
                    {
                        //Value = x.Id.ToString(),
                        Value = x.DestinationPlace.ToString(),
                        Text = x.ModuleType.Name + "(" + x.DestinationPlace + ")"
                        //Text = x.Name+(x.SerialNum!=null?x.SerialNum:"")+"( от "+x.CreatedDate+ ")"
                    }).ToList();
            var modulesStartEmpty = new SelectListItem()
            {
                Value = null,
                Text = "Выберите место"
            };
            modules.Insert(0, modulesStartEmpty);
            return Json(new SelectList(modules, "Value", "Text"));
        }

        private SelectList GetProject(int? selectedValue = null)
        {
            var projects = _context.Projects.AsNoTracking()
                .OrderBy(n => n.CreatedDate)
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name + "(" + x.Code + ")"
                    }).ToList();
            var projectEmptyItem = new SelectListItem()
            {
                Value = null,
                Text = "Выберите проект"
            };
            projects.Insert(0, projectEmptyItem);
            if (selectedValue != null && projects.Any(x => x.Value == selectedValue.ToString()))
            {
                return new SelectList(projects, "Value", "Text", selectedValue);
            }

            return new SelectList(projects, "Value", "Text");
        }

        private SelectList GetSubProject(int projectId, int? selectedValue = null)
        {
            var subProjects = _context.SubProjects.AsNoTracking()
                .OrderBy(n => n.CreatedDate)
                .Where(x => x.ProjectId == projectId)
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name + "(№ " + x.Code + ")"
                    }).ToList();
            if (selectedValue != null && subProjects.Any(x => x.Value == selectedValue.ToString()))
            {
                return new SelectList(subProjects, "Value", "Text", selectedValue);
            }

            return new SelectList(subProjects, "Value", "Text");
        }

        private SelectList GetProducts(int subProjectId, int? selectedValue = null)
        {
            var products = _context.Products.AsNoTracking()
                .OrderBy(n => n.CreatedDate)
                .Where(x => x.SubProjectId == subProjectId)
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name + "(№ " + (x.SerialNum ?? " ") + ")"
                    }).ToList();
            if (selectedValue != null && products.Any(x => x.Value == selectedValue.ToString()))
            {
                return new SelectList(products, "Value", "Text", selectedValue);
            }

            return new SelectList(products, "Value", "Text");
        }

        private SelectList GetOrderCards(int productId, int? selectedValue = null)
        {
            var cardOrders = _context.OrderCards.AsNoTracking()
                .OrderBy(n => n.CreatedDate)
                .Where(x => x.ProductId == productId)
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name + "(№ " + (x.Number ?? " ") + ")"
                    }).ToList();
            if (selectedValue != null && cardOrders.Any(x => x.Value == selectedValue.ToString()))
            {
                return new SelectList(cardOrders, "Value", "Text", selectedValue);
            }

            return new SelectList(cardOrders, "Value", "Text");
        }

        private SelectList GetModules(int orderCardId, int moduleTypeId, int? selectedValue = null)
        {
            var modules = _context.Modules.AsNoTracking()
                .OrderBy(n => n.CreatedDate)
                .Where(x => x.DestinationOrderCardId == orderCardId &&
                            (x.ActualOrderCardId == null || x.ActualOrderCardId == x.DestinationOrderCardId) &&
                            x.ModuleTypeId == moduleTypeId)
                .Select(x =>
                    new SelectListItem
                    {
                       // Value = x.Id.ToString(),
                        Value = x.DestinationPlace.ToString(),
                        Text = x.DestinationPlace.ToString()
                    }).ToList();
            if (selectedValue != null && modules.Any(x => x.Value == selectedValue.ToString()))
            {
                return new SelectList(modules, "Value", "Text", selectedValue);
            }

            return new SelectList(modules, "Value", "Text");
        }
    }
}