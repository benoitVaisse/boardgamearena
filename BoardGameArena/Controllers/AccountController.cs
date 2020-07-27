using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameArena.ViewModels;
using BoardGameArena.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using BoardGameArena.Repositories;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Primitives;
using MailKit;
using NETCore.MailKit.Core;

namespace BoardGameArena.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;
        private readonly PartRepository partRepo;
        private readonly IWebHostEnvironment host;
        private readonly IEmailService mailService;

        public AccountController(UserManager<Player> UserManager , SignInManager<Player> SignInManager, PartRepository PartRepo, IWebHostEnvironment host, IEmailService mailService)
        {
            this.userManager = UserManager;
            this.signInManager = SignInManager;
            partRepo = PartRepo;
            this.host = host;
            this.mailService = mailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(FormRegister FormRegister)
        {
            if (ModelState.IsValid)
            {
                var user = new Player()
                {
                    Email = FormRegister.Email,
                    UserName = FormRegister.UserName
                };

                var Result = await this.userManager.CreateAsync(user, FormRegister.Password);
                if (Result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, "Utilisateur");

                    var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                    var link = Url.Action("EmailVerify", "Account", new {userId = user.Id, token}, Request.Scheme, Request.Host.ToString());

                    await mailService.SendAsync(user.Email, "Email Verification", $"<a href=\"{link}\">click to verify </a>", true);

                    return RedirectToAction("EmailVerification", "Account");
                }
                else
                {
                    foreach(var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View();
        }

        public IActionResult EmailVerification()
        {
            return View();
        }

        public async Task<IActionResult> EmailVerify(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();
            var verify = await userManager.ConfirmEmailAsync(user, token);
            if (verify.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(FormLogin FormLogin)
        {
            Player Player = await this.userManager.FindByEmailAsync(FormLogin.Email);

            // si on a un joueur
            if(Player != null)
            {
                var confirmPassword = await this.userManager.CheckPasswordAsync(Player, FormLogin.Password);
                if (!Player.EmailConfirmed && confirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed");
                    return View();
                }

                var result = await this.signInManager.PasswordSignInAsync(Player.UserName, FormLogin.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError(string.Empty, "Email/password not valid");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email error");
            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MyAccount()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            this.ViewBag.User = user;
            this.ViewBag.parts = this.partRepo.GetPartOfOnePlayer(user);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            AccountEditViewModel model = new AccountEditViewModel()
            {
                UserName = user.UserName,
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(this.User);
                string UniqueName = null;
                if (model.Photo != null)
                {
                    string[] folder = new string[] { this.host.WebRootPath, "upload", user.Id.ToString() };
                    string PathFolder = Path.Combine(folder);
                    UniqueName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string FilePath = Path.Combine(PathFolder, UniqueName);
                    if (!Directory.Exists(PathFolder))
                    {
                        Directory.CreateDirectory(PathFolder);
                    }
                    model.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));

                    user.Photo = UniqueName;
                }
                user.UserName = model.UserName;
                user.Email = model.Email;
                var result = await this.userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyAccount", "Account");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(model);
        }
    }
}