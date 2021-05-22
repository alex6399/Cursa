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

            // var orderCards = _mapper.ProjectTo<OrderCardDisplayViewModel>(_context.OrderCards
            //     .AsNoTracking()
            //     .Where(p => p.ProductId == productId));
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
                // var searchNameValue = Request.Form["columns[2][search][value]"].FirstOrDefault();
                // var searchSerialNumValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                // var searchCertifiedNumValue = Request.Form["columns[4][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var id = Request.Form["productId"].FirstOrDefault();
                var productId = id != null ? Convert.ToInt32(id) : 0;

                var projectsData = _mapper.ProjectTo<OrderCardDisplayViewModel>(_context.OrderCards
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

            return View(new OrderCardCreateEditVM()
            {
                ProductId = product.Id,
                ModulesVM = modules
            });
        }

        // POST: OrderCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            //[Bind("Name,Number,Path,ProductId,Id")]
            [FromForm] OrderCardCreateEditVM orderCardVM)
        {
            if (ModelState.IsValid)
            {
                if (_context.OrderCards.Any(x => x.Number == orderCardVM.Number))
                {
                    ModelState.AddModelError("Number", "Такой cерийный № уже используется");
                }

                if (ModelState.IsValid)
                {
                    if (orderCardVM.ModulesVM == null)
                    {
                        ModelState.AddModelError(string.Empty, "Пустая конфигурация модулей!");
                        return View(orderCardVM);
                    }

                    var selectedModuleTypes = orderCardVM.ModulesVM
                        .Where(x => x.Addresses.Any(a => a))
                        .ToList();
                    // var selectedPlaces = new List<int>();
                    var selectedPlaces = new Dictionary<int, int>();
                    foreach (var moduleType
                        in selectedModuleTypes)
                    {
                        for (var i = 0; i < moduleType.Addresses.Length; i++)
                        {
                            if (moduleType.Addresses[i])
                            {
                                if (!selectedPlaces.ContainsKey(i))
                                {
                                    selectedPlaces.Add(i, moduleType.Id);
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
                    try
                    {
                        foreach (var kvp in selectedPlaces)
                        {
                            _context.Add(new Module()
                            {
                                // DestinationOrderCardId = orderCardVM.Id,
                                // ActualOrderCardId = orderCardVM.Id,
                                DestinationOrderCard = cardOrder,
                                ActualOrderCard = cardOrder,
                                ModuleTypeId = kvp.Value,
                                Place = kvp.Key
                            });
                        }

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

            ViewData["CreatedUserId"] = new SelectList(_context.Users, "Id", "Id", orderCard.CreatedUserId);
            ViewData["ModifiedUserId"] = new SelectList(_context.Users, "Id", "Id", orderCard.ModifiedUserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderCard.ProductId);
            return View(orderCard);
        }

        // POST: OrderCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Name,Number,Path,ProductId,EndDate,CreatedDate,ModifiedDate,CreatedUserId,ModifiedUserId,Id")]
            OrderCard orderCard)
        {
            if (id != orderCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderCardExists(orderCard.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(GetOrderCardsForProduct), new {productId = orderCard.ProductId});
            }

            ViewData["CreatedUserId"] = new SelectList(_context.Users, "Id", "Id", orderCard.CreatedUserId);
            ViewData["ModifiedUserId"] = new SelectList(_context.Users, "Id", "Id", orderCard.ModifiedUserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderCard.ProductId);
            return View(orderCard);
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
    }
}