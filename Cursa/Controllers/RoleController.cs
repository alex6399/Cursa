﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cursa.ViewModels.AccountVM;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cursa.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole<int>> _roleManager;
        UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole<int>> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(int id)
        {
            ViewBag.userId = id;

            var user = await _userManager.FindByIdAsync(Convert.ToString(id));

            if (user == null)
            {
                ViewBag.ErrorMessage =  $"Пользователь с Id= {id} не найден!";
                return NotFound();
            }

            var model = new List<UserRolesViewModel>();
            var roles = _roleManager.Roles.ToList();

            foreach (var role in roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(int id,List<UserRolesViewModel> model )
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Пользователь с Id= {id} не найден!";
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Роль не может быть удалена");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Роль не может быть добавлена");
                return View(model);
            }

            return RedirectToAction("Index","Users");
        }
    }
}
