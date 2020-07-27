using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameArena.Models;
using BoardGameArena.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameArena.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<Player> userManager;
        private readonly RoleManager<AppRole> roleManager;

        
        public AdminController(UserManager<Player> Usermanager, RoleManager<AppRole> RoleManager)
        {
            this.userManager = Usermanager;
            this.roleManager = RoleManager;
        }

        //---------------------- Index ---------------------------

        public IActionResult Index()
        {
            return View();
        }

        //------------------------------------------- Roles ---------------------------------------------------

        /// <summary>
        ///  page de creation d'un roles
        /// </summary>
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        /// <summary>
        /// Creation d'un role
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole(FormRoleAdminViewModel Model)
        {
            if (ModelState.IsValid)
            {
                AppRole Role = new AppRole()
                {
                    Name = Model.Name
                };

                var Result = await this.roleManager.CreateAsync(Role);
                if (Result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }

                // si non valide on affiche les erreur
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(Model);
        }

        /// <summary>
        /// listing des roles
        /// </summary>
        /// <returns></returns>
        public IActionResult ListRole()
        {
            var Roles = this.roleManager.Roles;
            return View(Roles);
        }


        [HttpGet("edit-role/{id}", Name ="edit-role")]
        public async Task<IActionResult> EditRole(int Id)
        {
            var Role = await this.roleManager.FindByIdAsync(Id.ToString());

            if (Role != null)
            {
                AdminEditRoleViewModel ModelRole = new AdminEditRoleViewModel()
                {
                    Id = Role.Id,
                    Name = Role.Name,
                    Users = await this.userManager.GetUsersInRoleAsync(Role.Name)
                };

                return View(ModelRole);
            }


            return RedirectToAction("ListRole");

        }

        [HttpPost("edit-role/{id}", Name = "edit-role")]
        public async Task<IActionResult> EditRole(AdminEditRoleViewModel model)
        {
            var Role = await this.roleManager.FindByIdAsync(model.Id.ToString());
            model.Users = await this.userManager.GetUsersInRoleAsync(Role.Name);

            if (Role != null)
            {
                Role.Name = model.Name;

                var result = await this.roleManager.UpdateAsync(Role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                return View(model);
            }
            else
            {
                ViewBag.ErrorMessage = "error";
                return View("Error");
            }

        }
    }
}