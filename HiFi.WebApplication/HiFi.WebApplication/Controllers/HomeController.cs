using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiFi.WebApplication.Models;
using HiFi.Common;

namespace HiFi.WebApplication.Controllers
{
    //[Route("Home")] 
    public class HomeController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        public HomeController()
        {

        }
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userData = User.Identity.Name;
                string authenticationType = HttpContext.User.Identity.AuthenticationType;
                string roleName = User.Claims.
                    Where(t => t.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").
                    Select(a => a.Value).FirstOrDefault();

                if (userData != null && userData != "")
                {
                    if (string.Equals(roleName.ToLower(), SD.AdminEndUser.ToLower(), StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard1", "Dashboards", new { Area = "Admin" });
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        //public IActionResult Dashboard1()
        //{
        //    return View();
        //}
    }
}
