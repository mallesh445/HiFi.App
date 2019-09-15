using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiFi.Data.Models;
using HiFi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManufacturerController : Controller
    {
        private readonly ILogger<ManufacturerController> _logger;
        private readonly IManufacturerService _manufacturerService;
        public ManufacturerController(IManufacturerService manufacturerService, ILogger<ManufacturerController> logger)
        {
            _manufacturerService = manufacturerService;
            _logger = logger;
        }
        // GET: Manufacturer
        public IActionResult Index()
        {
            var manfacturersList = _manufacturerService.GetAllManufacturers();
            return View(manfacturersList);
        }

        // GET: Manufacturer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            Manufacturer manufacturer = new Manufacturer() { CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
            return View(manufacturer);
        }

        // POST: Manufacturer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manufacturer manufacturer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _manufacturerService.InsertManufacturer(manufacturer);
                    return RedirectToAction("Index");
                }
                //return View(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Manufacturer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manufacturer/Edit/5
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

        // GET: Manufacturer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manufacturer/Delete/5
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