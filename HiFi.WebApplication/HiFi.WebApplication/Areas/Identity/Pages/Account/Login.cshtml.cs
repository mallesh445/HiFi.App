using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HiFi.Data.Models;
using Microsoft.AspNetCore.Http;
using HiFi.Data.Data;
using HiFi.Common;

namespace HiFi.WebApplication.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDBContext _db;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, ApplicationDBContext dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            _db = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                try
                {
                    if (result.Succeeded)
                    {
                        var user = _db.Users.Where(u => u.Email == Input.Email).FirstOrDefault();
                        //var count = _db.ShoppingCart.Where(u => u.ApplicationUserId == user.Id).ToList().Count();
                        var roleResult = from c in _db.UserRoles
                                         join p in _db.Roles on c.RoleId equals p.Id
                                         where c.UserId == user.Id
                                         select new List<string> { p.Name, c.RoleId };
                        string roleName = (from c in _db.UserRoles
                                           join p in _db.Roles on c.RoleId equals p.Id
                                           where c.UserId == user.Id
                                           select p.Name).FirstOrDefault();
                        //HttpContext.Session.SetInt32("CartCount", count);
                        _logger.LogInformation("User logged in.");
                        if (roleName.ToLower() == SD.AdminEndUser.ToLower())
                        {
                            return RedirectToAction("Dashboard1", "Dashboards", new { Area = "Admin" });
                        }
                        else if (roleName.ToLower() == SD.CustomerEndUser.ToLower())
                        {
                            return RedirectToAction("Index", "Home", new { Area = "" });
                        }
                        else
                        {
                            return RedirectToPage("./AccessDenied");
                        }
                        //return RedirectToAction("Index", "Default", new { Area = "Admin" });
                        return LocalRedirect(returnUrl);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
