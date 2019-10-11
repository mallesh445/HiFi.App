using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiFi.Services;
using HiFi.WebApplication.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DashboardsController : Controller
    {
        private readonly ISalesOrderService _salesOrderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public DashboardsController(ISalesOrderService salesOrderService, IUserService userService, IProductService productService)
        {
            _salesOrderService = salesOrderService;
            _userService = userService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard1");
        }

        public async Task<IActionResult> Dashboard1()
        {
            DashboardViewModel dashboard = new DashboardViewModel();

            dashboard.administrators_count = 5;
            dashboard.customers_count = await _userService.TotalUsersCount();
            dashboard.products_count = await _productService.ProductsCount();
            dashboard.orders_count =await _salesOrderService.TotalOrdersCount();
            return View(dashboard);
        }

        public IActionResult Dashboard2()
        {
            return View();
        }

    }
}