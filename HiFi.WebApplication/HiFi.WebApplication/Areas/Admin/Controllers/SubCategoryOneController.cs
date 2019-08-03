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

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryOneController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryOneController(ApplicationDBContext context, ISubCategoryService subCategoryService)
        {
            _context = context;
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

            return View(subCategoryOne);
        }

        // GET: Admin/SubCategoryOne/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SubCategoryOne/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryOne subCategoryOne, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subCategoryOne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subCategoryOne);
        }

        // GET: Admin/SubCategoryOne/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoryOne = await _context.SubCategoryOne.FindAsync(id);
            if (subCategoryOne == null)
            {
                return NotFound();
            }
            return View(subCategoryOne);
        }

        // POST: Admin/SubCategoryOne/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubCategoryOneId,SubCategoryName,Description,DisplayOrder,CreatedDate,UpdatedDate,IsActive,SC_ImageName,SC_ImagePath,EId")] SubCategoryOne subCategoryOne)
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
    }
}
