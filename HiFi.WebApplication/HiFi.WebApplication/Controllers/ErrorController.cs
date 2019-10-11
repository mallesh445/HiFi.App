using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HiFi.WebApplication.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Controllers
{
    public class ErrorController : Controller
    {

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (code)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the resource could not be found";
                    ViewBag.RouteOfException = statusCodeData?.OriginalPath;
                    return View("NotFound");
                case 500:
                    ViewBag.ErrorMessage = "Sorry something went wrong on the server";
                    ViewBag.RouteOfException = statusCodeData?.OriginalPath;
                    break;
                default:
                    ViewBag.ErrorMessage = "Sorry the resource could not found";
                    break;
            }
            // handle different codes or just return the default error view
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Error404()
        {
            return View();
        }
    }
}