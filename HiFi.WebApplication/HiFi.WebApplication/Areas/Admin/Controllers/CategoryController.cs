using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HiFi.Common;
using HiFi.Common.ExcelModel;
using HiFi.Data.Models;
using HiFi.Services.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _logger = logger;
            _categoryService=categoryService;
        }
        // GET: Category
        public ActionResult Index()
        {
            var data = _categoryService.GetAllCategories();
            return View(data);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            Category category = new Category() { CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
            return View(category);
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result= _categoryService.InsertCategory(category);
                    return RedirectToAction("Index");
                }
                //return View(category);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, new[] { "Create", "CategoryController" });
                return View();
            }
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    bool result = _categoryService.UpdateCategory(category);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new[] { "Create", "CategoryController" });
                return View();
            }
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _category = await _categoryService.GetCategoryByIdAsync(id);
            if (_category == null)
            {
                return NotFound();
            }
            return View(_category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _categoryService.DeleteCategory(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new[] { "Create", "CategoryController" });
                return View();
            }
        }

        /// <summary>
        /// Importing Categories.
        /// </summary>
        /// <param name="postedExcelFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImportCategories(IFormFile postedExcelFile)
        {
            if (ModelState.IsValid)
            {
                if (postedExcelFile == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                    TempData["Error"] = "Please Upload Your file.";
                    return RedirectToAction("Index");
                }
                else if (postedExcelFile.Length > UtilityConstants.MaxContentLength)
                {
                    TempData["Error"] = "SizeExceed";
                    return RedirectToAction("Index");
                }
                else if (postedExcelFile.Length > 0)
                {
                    string path = ExcelHelper.SavePathForThePostedFile(postedExcelFile);

                    if (!(path.Contains("xlsx") || path.Contains("xls")))
                        return Content("FileFormatError");

                    try
                    {
                        List<CategoryImportExcel> records = ExcelHelper.ReadSheet<CategoryImportExcel>(path, true, 0, null, true).ToList();
                        records = records.Where(r => !string.IsNullOrEmpty(r.CategoryName) && !string.IsNullOrEmpty(r.CreatedByUser)).ToList();
                        if (records.Count > 0)
                        {
                            string userId = User.Claims.
                                Where(t => t.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").
                                Select(a => a.Value).FirstOrDefault();
                            //objCategoryBO.InsertCategoryInBulk(records);
                            bool result = _categoryService.InsertCategorInBulk(records, userId);
                            System.IO.File.Delete(path);
                            TempData["Success"] = $"\"NumberOfRecords Uploaded\" : {records.Count()}";
                            return RedirectToAction("Index");
                        }
                        System.IO.File.Delete(path);
                        TempData["Success"] = $"\"NumberOfRecords Uploaded\" : 0";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message, new[] { "ImportCategories", "CategoryController" });
                        if (ex.Message.Contains("InValidZipCode"))
                        {
                            return Content(ex.Message);
                        }
                    }
                }
            }
            return Content("Error");
        }

    }
}