using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class EmailAccountController : Controller
    {
        // GET: EmailAccount
        public ActionResult List()
        {
            return View();
        }

        // GET: EmailAccount/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmailAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailAccount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmailAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmailAccount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmailAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmailAccount/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }
    }
}