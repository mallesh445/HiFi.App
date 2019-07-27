using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Controllers
{
    public class CategoryController : Controller
    {

        //private readonly ICategoryService _categoryService;
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        
            //if (TempData["Success"] != null)
            //{
            //    TempData["Success"] = TempData["Success"];
            //}
            //if (TempData["Error"] != null)
            //    TempData["Error"] = TempData["Error"];
            //return View();
}
}