using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Cursa.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // public IActionResult Index() => View();
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewBag.returnUrl = returnUrl;
           return  View();
        }

       
    }
}