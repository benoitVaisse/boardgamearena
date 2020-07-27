using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BoardGameArena.Data;
using BoardGameArena.Models;
using BoardGameArena.Repositories;
using BoardGameArena.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameArena.Controllers
{
    public class GameController : Controller
    {
        private readonly GameRepository gameRepository;
        private readonly IWebHostEnvironment host;

        public GameController(GameRepository GameRepository, IWebHostEnvironment host)
        {
            this.gameRepository = GameRepository;
            this.host = host;
        }


        public ActionResult Index()
        {
            var listeGames = this.gameRepository.FindAll();
            return View(listeGames);
        }

        [HttpGet("admin/games", Name ="admin_games")]
        [Authorize(Roles ="Admin")]
        public ActionResult AdminIndex()
        {
            if (TempData["success"] != null)
            {
                this.ViewBag.success = TempData["success"];
            }
            var listeGames = this.gameRepository.FindAll();
            return View("AdminIndex",listeGames);
        }

        [HttpGet("admin/game/nouveau",Name ="admin_game_create_get")]
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            FormGameViewModel model = new FormGameViewModel();
            return View(model);
        }

        [HttpPost("admin/game/nouveau", Name = "admin_game_create_post")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                Game Game = new Game()
                {
                    Title = model.Title,
                    Rules = model.Rules,
                    Description = model.Description,
                    NbMin = model.NbMin,
                    NbMax = model.NbMax
                };

                string UniqueTitle = null;
                if(model.CoverImage != null)
                {
                    var PathFolder = Path.Combine(this.host.WebRootPath, "img", "game");
                    UniqueTitle = Guid.NewGuid().ToString() + "_" + model.CoverImage.FileName;
                    var FilePath = Path.Combine(PathFolder, UniqueTitle);
                    if (!Directory.Exists(PathFolder))
                    {
                        Directory.CreateDirectory(PathFolder);
                    }
                    model.CoverImage.CopyTo( new FileStream(FilePath, FileMode.Create));

                    Game.CoverImage = UniqueTitle;
                }

                this.gameRepository.Create(Game);
                this.TempData["success"] = $"Le jeux {Game.Title} a bien été crée été";
                return RedirectToRoute("admin_games");

            }
            return View(model);
        }

        [HttpGet("game/detail/{id:int}", Name ="GameDetail")]
        [HttpGet("admin/game/detail/{id:int}", Name ="AdminGameDetail")]
        public IActionResult Detail(int Id)
        {
            Game Game = this.gameRepository.FindOneById(Id);
            return View(Game);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/game/edit/{id:int}", Name ="AdminGameEdit")]
        public IActionResult Edit(int Id)
        {
            Game Game = this.gameRepository.FindOneById(Id);
            if(Game != null)
            {
                FormGameViewModel model = new FormGameViewModel()
                {
                    Id = Game.Id,
                    Title = Game.Title,
                    NbMax = Game.NbMax,
                    NbMin = Game.NbMin,
                    Description = Game.Description,
                    Rules = Game.Rules
                };

                this.ViewBag.Game = Game;
                return View(model);

            }

            return BadRequest();
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("admin/game/update/", Name = "AdminUpdateGame")]
        public IActionResult Update(FormGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                Game Game = this.gameRepository.FindOneById(model.Id);
                if (Game != null)
                {

                    string UniqueCoverImage = Guid.NewGuid().ToString() + "_" + model.CoverImage.FileName;
                    var folderPath = Path.Combine(this.host.WebRootPath, "img", "game");
                    var PathFile = Path.Combine(folderPath, UniqueCoverImage);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    model.CoverImage.CopyTo(new FileStream(PathFile, FileMode.Create));
                    Game.Title = model.Title;
                    Game.NbMin = model.NbMin;
                    Game.NbMax = model.NbMax;
                    Game.Rules = model.Rules;
                    Game.Description = model.Description;
                    Game.CoverImage = UniqueCoverImage;
                    this.gameRepository.Update(Game);
                }
                return RedirectToAction("Detail", new { Id = model.Id });

            }
            return View(model);
        }

        [HttpGet("admin/game/delete/{id:int}", Name ="admin_game_delete")]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            Game Game = this.gameRepository.FindOneById(id);
            Game = this.gameRepository.Delete(Game);
            this.TempData["success"] = $"Le jeu {Game.Title} bien été supprimé";
            return RedirectToRoute("admin_games"); 
        }

    }
}