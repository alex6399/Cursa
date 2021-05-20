using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cursa.ViewModels.AccountVM;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Cursa.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel {ReturnUrl = returnUrl});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(
                        userName: model.Email, model.Password,
                        isPersistent: model.RememberMe,
                        lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        if (user.IsPasswordChange)
                        {
                            await _signInManager.SignOutAsync();
                            return RedirectToAction(
                                actionName: "NecessarilyChangePassword",
                                controllerName: "Users",
                                routeValues: new {id = user.Id});
                        }

                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Твой аккаунт заблокирован на 10 минут");
                    }
                }

                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(actionName: "Login", controllerName: "Login");
        }
    }
}