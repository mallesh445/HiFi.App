using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HiFi.Data.Data;
using HiFi.Data.Models;
using Microsoft.AspNetCore.Http;
using HiFi.Services;
using HiFi.Services.Catalog;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using HiFi.Common;
using HiFi.WebApplication.Areas.Admin.ViewModels;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryOneController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryOneController(ApplicationDBContext context, IHostingEnvironment hostingEnvironment
            , ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        // GET: Admin/SubCategoryOne
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubCategoryOne.ToListAsync());
        }

        // GET: Admin/SubCategoryOne/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoryOne = await _context.SubCategoryOne
                .FirstOrDefaultAsync(m => m.SubCategoryOneId == id);
            if (subCategoryOne == null)
            {
                return NotFound();
            }
            SubCategoryOneViewModel viewModel = PrepareSubCategoryOneViewModelFromSubCategoryOneEntity(subCategoryOne);
            return View(viewModel);
        }

        // GET: Admin/SubCategoryOne/Create
        public IActionResult Create()
        {
            //ViewBag.CategoryList = new SelectList(_categoryService.GetAllCategories(), "CategoryId", "CategoryName");
            var catlist = _categoryService.GetAllCategories();
            var cvm = new List<CategoryViewModel>();
            foreach (var item in catlist)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    Description = item.Description
                };
                cvm.Add(categoryViewModel);
            }

            SubCategoryOneViewModel viewModel = new SubCategoryOneViewModel()
            {
                MyCategoryList = cvm
            };
            return View(viewModel);
        }

        // POST: Admin/SubCategoryOne/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryOneViewModel subCategoryVM, IFormFile file, IFormCollection fc)
        {
            if (ModelState.IsValid)
            {
                subCategoryVM.CategoryId = Convert.ToInt32(fc["CategoryId"]);
                SubCategoryOne subCategoryOne = PrepareSubCategoryOneFromViewModel(subCategoryVM);

                _context.Add(subCategoryOne);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    //var productImage = new ProductImage();
                    if (files[0] != null && files[0].Length > 0)
                    {
                        //when user uploads an image
                        var uploads = Path.Combine(webRootPath, "Images");
                        string uploadedImageName = files[0].FileName.Substring(0, files[0].FileName.LastIndexOf("."));
                        var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));
                        using (var filestream = new FileStream(Path.Combine(uploads, uploadedImageName + subCategoryOne.SubCategoryOneId + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }
                        subCategoryOne.SC_ImagePath = @"\Images\" + uploadedImageName + subCategoryOne.SubCategoryOneId + extension;
                        subCategoryOne.SC_ImageName = uploadedImageName;
                    }
                    else
                    {
                        //when user does not upload image
                        var uploads = Path.Combine(webRootPath, @"Images\" + SD.DefaultSubCategoryImage);
                        System.IO.File.Copy(uploads, webRootPath + @"\Images\" + subCategoryOne.SubCategoryName + subCategoryOne.CategoryId + ".PNG");
                        subCategoryOne.SC_ImagePath = @"\Images\" + subCategoryOne.CategoryId + ".PNG";
                        subCategoryOne.SC_ImageName = subCategoryOne.SubCategoryName;
                    }
                    var finalResult = await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subCategoryVM);
        }

        // GET: Admin/SubCategoryOne/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoryOne = await _context.SubCategoryOne.FindAsync(id);
            SubCategoryOneViewModel scViewModel = PrepareSubCategoryOneViewModelFromSubCategoryOneEntity(subCategoryOne);

            var catlist = _categoryService.GetAllCategories();
            var cvm = new List<CategoryViewModel>();
            foreach (var item in catlist)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    Description = item.Description
                };
                cvm.Add(categoryViewModel);
            }
            
            if (scViewModel == null)
            {
                return NotFound();
            }
            scViewModel.MyCategoryList = cvm;
            return View(scViewModel);
        }

        // POST: Admin/SubCategoryOne/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategoryOne subCategoryOne)
        {
            if (id != subCategoryOne.SubCategoryOneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCategoryOne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoryOneExists(subCategoryOne.SubCategoryOneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subCategoryOne);
        }

        // GET: Admin/SubCategoryOne/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoryOne = await _context.SubCategoryOne
                .FirstOrDefaultAsync(m => m.SubCategoryOneId == id);
            if (subCategoryOne == null)
            {
                return NotFound();
            }

            return View(subCategoryOne);
        }

        // POST: Admin/SubCategoryOne/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategoryOne = await _context.SubCategoryOne.FindAsync(id);
            _context.SubCategoryOne.Remove(subCategoryOne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryOneExists(int id)
        {
            return _context.SubCategoryOne.Any(e => e.SubCategoryOneId == id);
        }

        /// <summary>
        /// Prepare SubCategoryOne From SubCategoryViewModel
        /// </summary>
        /// <param name="subCategoryVM"></param>
        /// <returns></returns>
        private SubCategoryOne PrepareSubCategoryOneFromViewModel(SubCategoryOneViewModel subCategoryVM)
        {
            SubCategoryOne subCategoryOne = new SubCategoryOne();
            if (subCategoryVM != null)
            {
                subCategoryOne.CategoryId = subCategoryVM.CategoryId;
                subCategoryOne.SubCategoryName = subCategoryVM.SubCategoryName;
                subCategoryOne.Description = subCategoryVM.Description;
                subCategoryOne.DisplayOrder = subCategoryVM.DisplayOrder;
                subCategoryOne.IsActive = subCategoryVM.IsActive;
                subCategoryOne.CreatedDate = subCategoryVM.CreatedDate;
                subCategoryOne.UpdatedDate = subCategoryVM.UpdatedDate;
                subCategoryOne.SC_ImagePath = subCategoryVM.ImagePath;
                //Temporarly storing b'coz this column is expecting not null.so later we will update exact name.
                subCategoryOne.SC_ImageName = "Default";
            }
            return subCategoryOne;
        }

        private SubCategoryOneViewModel PrepareSubCategoryOneViewModelFromSubCategoryOneEntity(SubCategoryOne subCategoryOne)
        {
            SubCategoryOneViewModel subCategoryVM = new SubCategoryOneViewModel();
            if (subCategoryVM != null)
            {
                subCategoryVM.CategoryId = subCategoryOne.CategoryId;
                subCategoryVM.SubCategoryName = subCategoryOne.SubCategoryName;
                subCategoryVM.Description = subCategoryOne.Description;
                subCategoryVM.DisplayOrder = subCategoryOne.DisplayOrder;
                subCategoryVM.IsActive = subCategoryOne.IsActive;
                subCategoryVM.CreatedDate = subCategoryOne.CreatedDate;
                subCategoryVM.UpdatedDate = subCategoryOne.UpdatedDate;
                subCategoryVM.ImagePath = subCategoryOne.SC_ImagePath;
                //Temporarly storing b'coz this column is expecting not null.so later we will update exact name.
                subCategoryVM.ImageName = subCategoryOne.SC_ImageName;
                subCategoryVM.ImagePath = subCategoryOne.SC_ImagePath;
            }
            return subCategoryVM;
        }
    }
}
