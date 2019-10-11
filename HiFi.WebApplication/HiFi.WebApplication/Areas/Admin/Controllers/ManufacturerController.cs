using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HiFi.Data.Models;
using HiFi.Services;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _hostingEnvironment;
        public ManufacturerController(IManufacturerService manufacturerService, ILogger<ManufacturerController> logger
            ,IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _manufacturerService = manufacturerService;
            _logger = logger;
        }
        // GET: Manufacturer
        public async Task<IActionResult> Index()
        {
            var manfacturersList =await _manufacturerService.GetAllManufacturers();
            return View(manfacturersList);
        }

        // GET: Manufacturer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Manufacturer manufacturer= await _manufacturerService.GetManufacturerByIdAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
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
        public async Task<IActionResult> Create(Manufacturer manufacturer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultManufacturer = await _manufacturerService.InsertManufacturer(manufacturer);
                    if (resultManufacturer != null)
                    {
                        var finalResult =await _manufacturerService.UpdateManufacturer(CheckAndPrepareAnyImageUploaded(manufacturer));
                        _logger.LogInformation("Manufacturer created successfully");
                    }
                    return RedirectToAction("Index");
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message,new[] { "Create", "ManufacturerController" });
                return View();
            }
        }

        // GET: Manufacturer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int _productId = (Int32)id;
            Manufacturer manufacturer = await _manufacturerService.GetManufacturerByIdAsync(id);

            if (manufacturer == null)
                return NotFound();
            return View(manufacturer);
        }

        // POST: Manufacturer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Manufacturer manufacturer)
        {
            try
            {
                if (id != manufacturer.ManufacturerId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        manufacturer = CheckAndPrepareAnyImageUploaded(manufacturer);
                        var finalResult = _manufacturerService.UpdateManufacturer(manufacturer);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message, new[] { "Edit", "ManufacturerController" });
                        return View();
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(manufacturer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new[] { "Edit", "ManufacturerController" });
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

        /// <summary>
        /// Check And Prepare Any Image Uploaded by user.
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        private Manufacturer CheckAndPrepareAnyImageUploaded(Manufacturer manufacturer)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files[0] != null && files[0].Length > 0)
                {
                    //when user uploads an image
                    var uploads = Path.Combine(webRootPath, "Images");
                    string uploadedImageName = files[0].FileName.Substring(0, files[0].FileName.LastIndexOf("."));
                    var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));
                    using (var filestream = new FileStream(Path.Combine(uploads, uploadedImageName + manufacturer.ManufacturerId + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    manufacturer.ImagePath = @"\Images\" + uploadedImageName + manufacturer.ManufacturerId + extension;
                    //manufacturer.MetaKeywords = uploadedImageName;
                }
                else
                {
                    //when user does not upload image
                    //var uploads = Path.Combine(webRootPath, @"Images\" + SD.DefaultSubCategoryImage);
                    //System.IO.File.Copy(uploads, webRootPath + @"\Images\" + subCategoryOne.SubCategoryName + subCategoryOne.CategoryId + ".PNG");
                    //subCategoryOne.SC_ImagePath = @"\Images\" + subCategoryOne.CategoryId + ".PNG";
                    //subCategoryOne.SC_ImageName = subCategoryOne.SubCategoryName;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new[] { "PrepImage", "ManufacturerController" });
            }
            return manufacturer;
        }

    }
}