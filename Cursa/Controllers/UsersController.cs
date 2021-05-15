using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Cursa.ViewModels.AccountVM;
using Cursa.ViewModels.Users;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;


namespace Cursa.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        readonly UserManager<User> _userManager;
        private readonly EfDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(EfDbContext context, IMapper mapper,UserManager<User> userManager,ILogger<UsersController> logger)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Users
        public IActionResult Index() => View(new UsersViewModel());

        [HttpPost]
        public IActionResult GetUsers()
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
                var searchEmailValue = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var searchPhoneValue = Request.Form["columns[3][search][value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;

                var projectsData = _mapper.ProjectTo<UsersViewModel>(_context.Users);

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    projectsData = projectsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchGlobalValue))
                {
                    projectsData = projectsData.Where(m => m.FullName.Contains(searchGlobalValue)
                                                           || m.Email.Contains(searchGlobalValue)
                                                           || m.PhoneNumber.Contains(searchGlobalValue));
                }

                if (!string.IsNullOrEmpty(searchFullNameValue))
                {
                    projectsData = projectsData.Where(m => m.FullName.Contains(searchFullNameValue));
                }

                if (!string.IsNullOrEmpty(searchPhoneValue))
                {
                    projectsData = projectsData.Where(m => m.PhoneNumber.Contains(searchPhoneValue));
                }
                if (!string.IsNullOrEmpty(searchEmailValue))    
                {
                    projectsData = projectsData.Where(m => m.Email.Contains(searchEmailValue));
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
        public IActionResult Create() => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    UserName = model.Email,
                    IsLockout = true
                };
                var resultCreateNewUser = await _userManager.CreateAsync(user, model.Password);
                if (resultCreateNewUser.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    await _userManager.AddToRolesAsync(user, new[] {"Менеджер"});
                    return RedirectToAction("Index");
                }

                foreach (var error in resultCreateNewUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            if (user == null)
            {
                return NotFound();
            }


            return View(new DetailsUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        // GET: Users/Create
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Convert.ToString(model.Id));
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.MiddleName = model.MiddleName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            if (user == null)
            {
                return NotFound();
            }

            var model = new ChangePasswordViewModel
                {Id = user.Id, FullName = user.GetFullName};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Convert.ToString(model.Id));
                if (user != null)
                {
                    var passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as
                            IPasswordValidator<User>;
                    var passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    if (passwordValidator != null && passwordHasher != null)
                    {
                        var result =
                            await passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                        if (result.Succeeded)
                        {
                            user.PasswordHash = passwordHasher.HashPassword(user, model.NewPassword);
                            user.IsLockout = true;
                            await _userManager.UpdateAsync(user);
                            return RedirectToAction("Index");
                        }
                    }

                    ModelState.AddModelError(
                        string.Empty,
                        "Пароль должен содержать буквы верхнего и нижнего регистров, цифры и специальные символы");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> NecessarilyChangePassword(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            if (user == null)
            {
                return NotFound();
            }

            var model = new NecessarilyChangePasswordViewModel {Id = user.Id};
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> NecessarilyChangePassword(NecessarilyChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(Convert.ToString(model.Id));
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(
                        user,
                        model.OldPassword,
                        model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.IsLockout = false;
                        var resultUpdate = await _userManager.UpdateAsync(user);
                        if (resultUpdate.Succeeded)
                        {
                            return RedirectToAction("Login", "Login");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Неверный");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Неверный пароль!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }

            return View(model);
        }
    }
}
// TODO сделать блокировку пользователей