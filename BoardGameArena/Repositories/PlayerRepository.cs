using BoardGameArena.Data;
using BoardGameArena.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Repositories
{
    public class PlayerRepository
    {
        private BgaContext context;

        public PlayerRepository(BgaContext context)
        {
            this.context = context;
        }

        public IEnumerable<Player> FindAll()
        {
            var query = from p in context.Players
                        select p;

            return query.ToList();
        }

        public Player Create(Player Player)
        {
            context.Players.Add(Player);
            context.SaveChanges();
            return Player;
        }

        public Player FindOneByid(int id)
        {
            var Player = context.Players.Find(id);

            return Player;
        }

        public IEnumerable<Player> FindByid(int[] ids)
        {
            var Player = from p in context.Players.Where(p => ids.Contains(p.Id))
                         select p;

            return Player;
        }

        public Player Update(Player PlayerChange)
        {
            var OldPlayer = this.context.Players.Attach(PlayerChange);
            OldPlayer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return PlayerChange;
        }
    }
}
