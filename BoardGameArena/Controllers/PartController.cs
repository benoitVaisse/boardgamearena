using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGameArena.Data;
using BoardGameArena.Models;
using BoardGameArena.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BoardGameArena.Controllers
{
    public class PartController : Controller
    {
        private readonly UserManager<Player> userManager;
        private readonly GameRepository gameRepository;
        private readonly PlayerRepository playerRepository;
        private readonly PartRepository partRepository;

        public PartController(UserManager<Player> UserManager,GameRepository GameRepo, PlayerRepository PlayerRepo, PartRepository PartRepo)
        {
            userManager = UserManager;
            this.gameRepository = GameRepo;
            this.playerRepository = PlayerRepo;
            this.partRepository = PartRepo;
        }


        public IActionResult Index()
        {
            IEnumerable<Part> ListParts = this.partRepository.FindAll();
            return this.View(ListParts);
        }


        [HttpGet("/nouvelle/partie" ,Name = "new-part")]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Games = this.gameRepository.FindAll();
            ViewBag.Players = this.playerRepository.FindAll();

            return View();
        }

        [HttpPost("/nouvelle/partie", Name = "new-part-post")]
        [Authorize]
        public IActionResult Create(string title, int id_game, int[] id_players)
        {
            this.partRepository.Create(title, id_game, id_players);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet("partie/detail/{id:alphanumeric}")]
        public IActionResult Detail(int Id)
        {
            ViewBag.error = null;
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.error = TempData["ErrorMessage"];
            }
            Part Part = this.partRepository.findOneById(Id);
            return View(Part);
        }

        [HttpGet("partie/edit/{id:int}",Name ="getPartEdit")]
        [HttpGet("admin/partie/edit/{id:int}", Name = "getPartEditAdmin")]
        [Authorize]
        public IActionResult Edit(int Id)
        {
            Part Part = this.partRepository.findOneById(Id);
            if(Part.IsFinish && !this.User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "La partie est Terminée, Impossible de la modifier";
                return RedirectToAction("Detail", new { Id = Id });

            }
            else
            {
                return View(Part);
            }
        }

        [HttpPost("partie/edit/{id:int}", Name = "postPartEdit")]
        [HttpPost("admin/partie/edit/{id:int}", Name = "postPartEditAdmin")]
        public IActionResult Edit(int PartId, int[] score)
        {
            Part Part = this.partRepository.findOneById(PartId);
            if ((Part.IsFinish && this.User.IsInRole("Admin")) || !Part.IsFinish)
            {
                var Form = HttpContext.Request.Form;
                if (Part != null)
                {
                    this.partRepository.Update(Part, Form);
                }
                return RedirectToAction("Detail", new { Id = PartId });
            }
            return BadRequest();
        }
    }
}