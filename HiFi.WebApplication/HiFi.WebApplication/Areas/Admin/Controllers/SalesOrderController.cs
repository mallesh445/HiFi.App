using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiFi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SalesOrderController : Controller
    {
        private readonly ISalesOrderService _salesOrderService;
        private readonly ILogger<SalesOrderController> _logger;
        public SalesOrderController(ISalesOrderService salesOrderService,ILogger<SalesOrderController> logger)
        {
            _salesOrderService = salesOrderService;
            _logger = logger;
        }

        // GET: SalesOrder
        public async Task<IActionResult> Index()
        {
            var data =await _salesOrderService.GetAllSalesOrders();
            return View(data);
        }

        // GET: SalesOrder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesOrder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}