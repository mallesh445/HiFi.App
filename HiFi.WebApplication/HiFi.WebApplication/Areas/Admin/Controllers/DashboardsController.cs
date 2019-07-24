using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DashboardsController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard1");
        }

        public IActionResult Dashboard1()
        {
            return View();
        }

        public IActionResult Dashboard2()
        {
            return View();
        }

    }
}