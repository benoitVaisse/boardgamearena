using BoardGameArena.Data;
using BoardGameArena.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Repositories
{
    public class PartRepository
    {
        private BgaContext context;
        private PartPlayerRepository partPlayerRepository;
        private PlayerRepository playerRepository;

        public PartRepository(BgaContext context, PlayerRepository PlayerRepo, PartPlayerRepository PartPlayerRepo)
        {
            this.context = context;
            this.partPlayerRepository = PartPlayerRepo;
            this.playerRepository = PlayerRepo;
        }

        public IEnumerable<Part> FindAll()
        {
            var query = from p in context.Parts
                        .Include(Part => Part.Game)
                        .Include(Part => Part.PartPlayers)
                            .ThenInclude(PartPlayer => PartPlayer.Player)
                        select p;
            return query.ToList();
        }

        public Part findOneById(int Id)
        {
            var Part = context.Parts
                        .Where(p => p.Id == Id)
                        .Include(Part => Part.Game)
                        .Include(Part => Part.PartPlayers)
                            .ThenInclude(PartPlayer => PartPlayer.Player)
                        .FirstOrDefault();
            return Part;
        }

        public Part Create(string title, int IdGame, int[] id_player)
        {

            Part Part = new Part()
            {
                Title = title,
                Game = this.context.Games.Find(IdGame)
            };
            IEnumerable<Player> Players = this.playerRepository.FindByid(id_player);

            IEnumerable<PartPlayer> PartPlayers = this.partPlayerRepository.Create(Part, Players);

            Part.PartPlayers = PartPlayers;
            Part.CreatedAt = DateTime.Now;
            this.context.Parts.Add(Part);

            this.context.SaveChanges();

            return Part;
        }

        public Part Update(Part NewPart, IFormCollection Form)
        {
            bool IsFinish = false;
            if (Boolean.TryParse(Form["isFinish"], out IsFinish))
            {
                NewPart.IsFinish = IsFinish;
            }
            

            foreach(PartPlayer pp in NewPart.PartPlayers)
            {
                if (!string.IsNullOrEmpty(Form["score[" + pp.Player.Id + "]"]))
                {
                    pp.Score = int.Parse(Form["score[" + pp.Player.Id + "]"]);
                }
            }
            this.context.SaveChanges();
            return NewPart;
        }


        public IEnumerable<Part> GetPartOfOnePlayer(Player User)
        {
            //var Parts = context.Parts
            //            .Include(Part => Part.Game)
            //            .Include(Part => Part.PartPlayers
            //                .Where(PartPlayer => PartPlayer.PartId == 1))
            //                .ThenInclude(pp => pp.Player).ToList();


            var query = from p in context.Parts
                        join pp in context.PartPlayers on p.Id equals pp.PartId
                        join u in context.Players on pp.PlayerId equals u.Id
                        where u.Id == User.Id
                        select p;

            return query.ToList();
        }
    }
}
