using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameArena.Data;
using BoardGameArena.Models;
using BoardGameArena.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameArena.Controllers
{
    public class PlayerController : Controller
    {
        private readonly PlayerRepository playerRepository;
        private readonly UserManager<Player> userManager;

        public PlayerController(PlayerRepository PlayerRepo, UserManager<Player> userManager)
        {
            this.playerRepository = PlayerRepo;
            this.userManager = userManager;
            
        }

        public IActionResult Index()
        {
            IEnumerable<Player> ListePlayers = this.playerRepository.FindAll();
            return View(ListePlayers);
        }

        public IActionResult Detail(int Id)
        {
            Player Player = this.playerRepository.FindOneByid(Id);
            return View(Player);
        }


        //-----------------------ADMIN ------------------------------

        [HttpGet("admin/players", Name ="admin_players")]
        [Authorize(Roles ="Admin")]
        public IActionResult IndexAdmin()
        {
            if (TempData["success"] != null)
            {
                this.ViewBag.success = TempData["success"];
            }
            if (TempData["error"] != null)
            {
                this.ViewBag.error = TempData["error"];
            }

            IEnumerable<Player> Players = this.playerRepository.FindAll();
            return View(Players);
        }

        [HttpDelete("admin/player/delete/{id:int}", Name ="admin_player_delete")]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = await this.userManager.FindByIdAsync(Id.ToString());
            if(user != null)
            {
                if(user.PartPlayers.Count() == 0)
                {
                    var result = await this.userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        this.TempData["success"] = $"Le Joueur {user.UserName} a bien été supprimé";

                    }
                    else
                    {
                        TempData["error"] = "L'utilisateur n'a pas pu être supprimé";
                    }

                }
                else
                {
                    TempData["error"] = "L'utilisateur n'a pas pu être supprimé car il a joué une ou plusieurs partie";
                }
                
            }
            else
            {
                TempData["error"] = "L'utilisateur n'a pas été trouvé";
            }

            return RedirectToRoute("admin_players");
        }
    }
}