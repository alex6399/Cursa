using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Cursa.Controllers
{
    [Authorize]
    public class HomeAdminController : Controller
    {
        public IActionResult Index()=>View();
        }
}
