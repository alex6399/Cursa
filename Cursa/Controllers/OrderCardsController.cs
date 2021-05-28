using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Cursa.ViewModels.Base;
using Cursa.ViewModels.OrderCardVM;
using Cursa.ViewModels.ProductsVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Cursa.Controllers
{
    public class OrderCardsController : Controller
    {
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderCardsController> _logger;

        public OrderCardsController(EfDbContext context, IMapper mapper, ILogger<OrderCardsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: OrderCards
        // public async Task<IActionResult> Index()
        // {
        //     var efDbContext = _context.OrderCards.Include(o => o.CreatedUser).Include(o => o.ModifiedUser)
        //         .Include(o => o.Product);
        //     return View(await efDbContext.ToListAsync());
        // }

        // GET: OrderCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCard = await _context.OrderCards
                .Include(o => o.Employee)
                .Include(o => o.CreatedUser)
                .Include(o => o.ModifiedUser)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderCard == null)
            {
                return NotFound();
            }

            return View(orderCard);
        }

        [HttpGet]
        public IActionResult GetOrderCardsForProduct(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            return View(new OrderCardDisplayViewModel()
            {
                Product = new BaseViewModel()
                {
                    Id = product.Id,
                    Name = product.Name
                }
            });
        }

        [HttpPost]
        public IActionResult FindCardOrdersForProduct()
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
                var id = Request.Form["productId"].FirstOrDefault();
                var productId = id != null ? Convert.ToInt32(id) : 0;

                var projectsData = _mapper.ProjectTo<OrderCardDisplayViewModel>(_context.OrderCards
                    .Include(x => x.Employee)
                    .AsNoTracking()
                    .Where(p => p.ProductId == productId));

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.Name.Contains(searchGlobalValue)
                                                           || m.Number.Contains(searchGlobalValue));
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

        [HttpPost]
        public IActionResult GetCardOrdersItems()
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
                // var searchSerialNumValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                // var searchCertifiedNumValue = Request.Form["columns[4][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var id = Request.Form["OrderCardId"].FirstOrDefault();
                var orderCardId = id != null ? Convert.ToInt32(id) : 0;

                var projectsData = _context.Modules
                    .AsNoTracking()
                    .Include(x => x.ModuleType)
                    .Where(x => x.DestinationOrderCardId == orderCardId)
                    .ToList()
                    .GroupBy(x => x.ModuleType.Name)
                    .Select(x => new OrderCardSummaryModule
                    {
                        ModuleTypeName = x.Key,
                        Addresses = x.Select(m => m.Place)
                    });

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

        // GET: OrderCards/Create
        public async Task<IActionResult> Create(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.TitleProduct = "для : " + product.Name;
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            var systemUnit =
                await _context.ModulesTypes.FirstOrDefaultAsync(x => x.IsActiv && !x.IsCommunicationDevice);
            if (systemUnit == null)
            {
                ModelState.AddModelError("", "Отсутствуют системные модули");
                return View(new OrderCardCreateEditVM()
                {
                    ProductId = product.Id
                });
            }


            int addressLength = systemUnit.NumberConnectionPoints;

            var modules = _context.ModulesTypes
                .Where(x => x.IsActiv && x.IsCommunicationDevice)
                .Select(x => new OrderCardCreateEditModuleVM()
                    {Id = x.Id, Name = x.Name, Addresses = new bool[addressLength]})
                .ToList();
            ViewData["EmployeeId"] = new SelectList(_context.Employees
                .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");
            return View(new OrderCardCreateEditVM()
            {
                ProductId = product.Id,
                systemUnit = new BaseViewModel()
                {
                    Id = systemUnit.Id,
                    Name = systemUnit.Name
                },
                ModulesVM = modules
            });
        }

        // POST: OrderCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //[Bind("Name,Number,Path,ProductId,Id")]
        public async Task<IActionResult> Create([FromForm] OrderCardCreateEditVM orderCardVM)
        {
            if (ModelState.IsValid)
            {
                if (orderCardVM.ModulesVM != null && orderCardVM.systemUnit != null)
                {
                    var selectedModuleTypes = new List<OrderCardCreateEditModuleVM>();
                    var unSelectedModuleTypes = new List<int>();
                    // var selectedModuleTypes = orderCardVM.ModulesVM
                    //     .Where(x => x.Addresses.Any(a => a))
                    //     .ToList();
                    foreach (var moduleAddresses in orderCardVM.ModulesVM)
                    {
                        if (moduleAddresses.Addresses.Any(a => a))
                        {
                            selectedModuleTypes.Add(moduleAddresses);
                        }
                        else
                        {
                            unSelectedModuleTypes.Add(moduleAddresses.Id);
                        }
                    }

                    if (selectedModuleTypes.Count == 0)
                    {
                        ViewData["EmployeeId"] = new SelectList(_context.Employees
                            .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");
                        ModelState.AddModelError(string.Empty, "Пустая конфигурация модулей!");
                        return View(orderCardVM);
                    }

                    var selectedPlaces = new Dictionary<int, int>();
                    foreach (var moduleType in selectedModuleTypes)
                    {
                        for (int place = 1, i = 0; i < moduleType.Addresses.Length; i++, place++)
                        {
                            if (moduleType.Addresses[i])
                            {
                                if (!selectedPlaces.ContainsKey(place))
                                {
                                    selectedPlaces.Add(place, moduleType.Id);
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, "Неверная конфигурация модулей!");
                                    return View(orderCardVM);
                                }
                            }
                        }
                    }

                    var cardOrder = _mapper.Map<OrderCardCreateEditVM, OrderCard>(orderCardVM);
                    cardOrder.UnSelectedModuleTypes = unSelectedModuleTypes;
                    try
                    {
                        foreach (var kvp in selectedPlaces)
                        {
                            _context.Add(new Module()
                            {
                                DestinationOrderCard = cardOrder,
                                ActualOrderCard = cardOrder,
                                ModuleTypeId = kvp.Value,
                                Place = kvp.Key
                            });
                        }

                        _context.Add(new Module()
                        {
                            DestinationOrderCard = cardOrder,
                            ActualOrderCard = cardOrder,
                            ModuleTypeId = orderCardVM.systemUnit.Id,
                            Place = -1
                        });

                        _context.Add(cardOrder);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(GetOrderCardsForProduct),
                            new {productId = cardOrder.ProductId});
                    }
                    catch (DbUpdateException e)
                    {
                        var exception = e.InnerException;
                        if (exception != null && exception.Message.Contains("IX_OrderCards_Number"))
                        {
                            ModelState.AddModelError("Number", "Такой cерийный № уже используется");
                        }
                    }
                }
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees
                .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");

            // ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderCardDTO.ProductId);
            return View(orderCardVM);
        }

        // GET: OrderCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCard = await _context.OrderCards.FindAsync(id);
            if (orderCard == null)
            {
                return NotFound();
            }

            var systemUnit =
                await _context.Modules
                    .Include(x => x.ModuleType)
                    .FirstOrDefaultAsync(x => x.DestinationOrderCardId == id && !x.ModuleType.IsCommunicationDevice);
            if (systemUnit == null)
            {
                return NotFound();
            }

            {
                int addressLength = systemUnit.ModuleType.NumberConnectionPoints;

                var modules = _context.Modules
                    .Include(x => x.ModuleType)
                    .AsNoTracking()
                    .Where(x => x.DestinationOrderCardId == id && x.ModuleType.IsCommunicationDevice)
                    .ToList();

                var cardModules = modules.GroupBy(x => x.ModuleType.Id)
                    .Select(x => new OrderCardCreateEditModuleVM
                    {
                        Id = x.Key,
                        Name = x.First().ModuleType.Name,
                        Addresses = SetAddress(x.Select(m => --m.Place), addressLength)
                    })
                    .ToList();
                var unSelectMod = _context.ModulesTypes.Where(x => orderCard.UnSelectedModuleTypes.Contains(x.Id))
                    .ToList();
                var unSelectModAddresses = unSelectMod.Select(x => new OrderCardCreateEditModuleVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Addresses = new bool[addressLength]
                }).ToList();
                cardModules.AddRange(unSelectModAddresses);
                ViewData["EmployeeId"] = new SelectList(_context.Employees
                    .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");
                return View(new OrderCardCreateEditVM()
                {
                    ProductId = orderCard.ProductId,
                    Name = orderCard.Name,
                    Number = orderCard.Number,
                    systemUnit = new BaseViewModel()
                    {
                        Id = systemUnit.Id,
                        Name = systemUnit.ModuleType.Name
                    },
                    ModulesVM = cardModules
                });
            }
        }

        private static bool[] SetAddress(IEnumerable<int> places, int addressLength)
        {
            var addresses = new bool[addressLength];
            foreach (var place in places)
            {
                addresses[place] = true;
            }

            return addresses;
        }

        // POST: OrderCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            //[Bind("Name,Number,Path,ProductId,EndDate,CreatedDate,ModifiedDate,CreatedUserId,ModifiedUserId,Id")]
            OrderCardCreateEditVM orderCardVM)
        {
            if (id != orderCardVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && orderCardVM.ModulesVM != null)
            {
                var selectedModuleTypes = new List<OrderCardCreateEditModuleVM>();
                var unSelectedModuleTypes = new List<int>();
                // var selectedModuleTypes = orderCardVM.ModulesVM
                //     .Where(x => x.Addresses.Any(a => a))
                //     .ToList();
                foreach (var moduleAddresses in orderCardVM.ModulesVM)
                {
                    if (moduleAddresses.Addresses.Any(a => a))
                    {
                        selectedModuleTypes.Add(moduleAddresses);
                    }
                    else
                    {
                        unSelectedModuleTypes.Add(moduleAddresses.Id);
                    }
                }

                /// + добавить не выбранные модули
                if (selectedModuleTypes.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "Пустая конфигурация модулей!");
                    return View(orderCardVM);
                }

                var selectedPlaces = new Dictionary<int, int>();
                foreach (var moduleType in selectedModuleTypes)
                {
                    for (int place = 1, i = 0; i < moduleType.Addresses.Length; i++, place++)
                    {
                        if (moduleType.Addresses[i])
                        {
                            if (!selectedPlaces.ContainsKey(place))
                            {
                                selectedPlaces.Add(place, moduleType.Id);
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Неверная конфигурация модулей!");
                                return View(orderCardVM);
                            }
                        }
                    }
                }

                var cardOrder = _mapper.Map<OrderCardCreateEditVM, OrderCard>(orderCardVM);
                cardOrder.UnSelectedModuleTypes = unSelectedModuleTypes;

                var modulesUpdate = new List<Module>();
                foreach (var kvp in selectedPlaces)
                {
                    modulesUpdate.Add(new Module()
                    {
                        // тут нужно еще как-то id получить модуля из БД
                        DestinationOrderCard = cardOrder,
                        ActualOrderCard = cardOrder,
                        ModuleTypeId = kvp.Value,
                        Place = kvp.Key
                    });
                }

                cardOrder.Modules = modulesUpdate;
                //cardOrder.Modules.Add();
                try
                {
                    _context.Update(cardOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderCardExists(cardOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(GetOrderCardsForProduct), new {productId = orderCardVM.ProductId});
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees
                .Where(x => x.Department.IsResponsibleDesignWork), "Id", "GetFullName");
            // ViewData["CreatedUserId"] = new SelectList(_context.Users, "Id", "Id", orderCard.CreatedUserId);
            // ViewData["ModifiedUserId"] = new SelectList(_context.Users, "Id", "Id", orderCard.ModifiedUserId);
            // ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderCard.ProductId);
            return View(orderCardVM);
        }

        // GET: OrderCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderCard = await _context.OrderCards
                .Include(o => o.CreatedUser)
                .Include(o => o.ModifiedUser)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderCard == null)
            {
                return NotFound();
            }

            return View(orderCard);
        }

        // POST: OrderCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderCard = await _context.OrderCards.FindAsync(id);
            _context.OrderCards.Remove(orderCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetOrderCardsForProduct), new {productId = orderCard.ProductId});
        }

        private bool OrderCardExists(int id)
        {
            return _context.OrderCards.Any(e => e.Id == id);
        }

        [HttpGet]
        public JsonResult IsNumberOrderCardExist(string Number, int? Id)
        {
            if (Id == null)
            {
                return Json(!_context.OrderCards.Any(x => x.Number == Number));
            }
            else
            {
                return Json(!_context.OrderCards.Any(x => x.Number == Number
                                                          && x.Id != Id));
            }
        }

        public IActionResult GetCardOrders(int productId)
        {
            var cardOrders = _context.OrderCards.AsNoTracking()
                .OrderBy(n => n.CreatedDate)
                .Where(x => x.ProductId == productId)
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name + (x.Number ?? "") + "( от " + x.CreatedDate + ")"
                        //Text = x.Name+(x.SerialNum!=null?x.SerialNum:"")+"( от "+x.CreatedDate+ ")"
                    }).ToList();
            var cardOrderStartEmpty = new SelectListItem()
            {
                Value = null,
                Text = "Выбирете карту заказа"
            };
            cardOrders.Insert(0, cardOrderStartEmpty);
            return Json(new SelectList(cardOrders, "Value", "Text"));
        }
    }
}