
using BoardGameArena.Data;
using BoardGameArena.Models;
using BoardGameArena.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Repositories
{
    public class GameRepository : IGameRepository
    {
        private BgaContext context;

        public GameRepository(BgaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Game> FindAll()
        {
            var query = from g in context.Games
                        .Include(Game => Game.Parts)
                        select g;

            return query.ToList();
        }

        public Game Create(Game Game)
        {
            context.Games.Add(Game);
            context.SaveChanges();
            return Game;
        }

        public Game FindOneById(int Id)
        {
            Game Game = context.Games.Find(Id);

            return Game;
        }


        public Game Update(Game GameChange) {

            var oldGame = this.context.Attach(GameChange);
            oldGame.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();

            return GameChange;
        }


        public Game Delete(Game Game)
        {
            var GameRemoved = this.context.Games.Remove(Game);
            this.context.SaveChanges();
            return Game;
        }
    }
}
