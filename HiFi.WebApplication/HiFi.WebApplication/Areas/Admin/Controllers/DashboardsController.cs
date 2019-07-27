using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiFi.WebApplication.Areas.Admin.ViewModels;
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
            DashboardViewModel dashboard = new DashboardViewModel();

            dashboard.administrators_count = 5;
            dashboard.customers_count = 20;
            dashboard.productss_count = 86;
            dashboard.orders_count = 54;
            return View(dashboard);
        }

        public IActionResult Dashboard2()
        {
            return View();
        }

    }
}