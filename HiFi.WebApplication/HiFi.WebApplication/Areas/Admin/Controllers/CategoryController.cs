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

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }
        // GET: Category
        public ActionResult Index()
        {
            var data = _categoryService.GetAllCategories();
            return View(data);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
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
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
                    bool result = _categoryService.InsertCategory(category);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
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
                            //objCategoryBO.InsertCategoryInBulk(records);
                            bool result = _categoryService.InsertCategorInBulk(records);
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