using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiFi.Data.Models;
using HiFi.WebApplication.Areas.Admin.ViewModels;
using HiFi.WebApplication.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [TypeFilter(typeof(CustomFilterWithDI))]
    [Authorize]
    [Area("Admin")]
    public class AdministratorController : Controller
    {
        public readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministratorController(RoleManager<IdentityRole> role
            , UserManager<ApplicationUser> userManager)
        {
            this.roleManager = role;
            this.userManager = userManager;
        }

        // GET: Administrator
        public ActionResult Index()
        {
            return RedirectToAction("ListUsers");
        }

        // GET: Administrator
        public ActionResult Roles()
        {
            return RedirectToAction("ListRoles");
        }
       
        [HttpGet]
        // GET: Administrator/ListUsers
        public async Task<IActionResult> ListUsers()
        {
            var usersList = userManager.Users;
            return View(usersList);
        }

        [HttpGet]
        // GET: Administrator/ListRoles
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        // GET: Administrator/Create
        public IActionResult CreateRole()
        {
            return View();
        }

        // POST: Administrator/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleViewModel roleviewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = roleviewModel.RoleName
                    };

                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(ListRoles));
                    }

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(roleviewModel);
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrator/Edit/5
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={id} cannot be found.";
                return View("Notfound");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var _user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(_user, role.Name))
                {
                    model.Users.Add(_user.UserName);
                }
            }

            return View(model);
        }

        // POST: Administrator/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(editRoleViewModel.Id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id={editRoleViewModel.Id} cannot be found.";
                    return View("Notfound");
                }
                else
                {
                    role.Name = editRoleViewModel.RoleName;
                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(editRoleViewModel);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrator/Edit/5
        public async Task<IActionResult> EditUser(string id)
        {
            var applicationUser = await userManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id={id} cannot be found.";
                return View("Notfound");
            }
            var model = new ApplicationUser
            {
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber
            };
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string Id)
        {
            ViewBag.roleId = Id;
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={Id} cannot be found.";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleviewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleviewModel.IsSelected = true;
                }
                else
                {
                    userRoleviewModel.IsSelected = false;
                }
                model.Add(userRoleviewModel);
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(string roleId,List<UserRoleViewModel> model)
        {
            //roleId is not coming
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await roleManager.FindByIdAsync(roleId);
                    if (role == null)
                    {
                        ViewBag.ErrorMessage = $"Role with Id={roleId} cannot be found.";
                        return View("Notfound");
                    }

                    for (int i = 0; i < model.Count; i++)
                    {
                        var user = await userManager.FindByIdAsync(model[i].UserId);
                        IdentityResult result = null;
                        bool isInRole = await userManager.IsInRoleAsync(user, roleId);
                        if (model[i].IsSelected && !isInRole)
                        {
                            result = await userManager.AddToRoleAsync(user, role.Name);
                        }
                        else if (!model[i].IsSelected && isInRole)
                        {
                            result = await userManager.RemoveFromRoleAsync(user, role.Name);
                        }
                        else
                        {
                            continue;
                        }
                        if (result.Succeeded)
                        {
                            if (i < model.Count - 1)
                            {
                                continue;
                            }
                            else
                            {
                                return RedirectToAction("EditRole", new { Id = roleId });
                            }
                        }
                    }
                    return RedirectToAction("EditRole", new { Id = roleId });
                }
                catch (Exception ex)
                {
                    throw ex;
                } 
            }
            return View();
        }

        // GET: Administrator/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Administrator/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Administrator/Delete/5
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
    }
}