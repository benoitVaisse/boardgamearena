using BoardGameArena.Data;
using BoardGameArena.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Repositories
{
    public class PartPlayerRepository
    {
        private BgaContext context;

        public PartPlayerRepository(BgaContext context)
        {
            this.context = context;
        }

        public IEnumerable<PartPlayer> Create(Part Part, IEnumerable<Player> Players)
        {
            List<PartPlayer> ListPartPlayer = new List<PartPlayer>();
            foreach(Player Player in Players)
            {

                PartPlayer PartPlayer = new PartPlayer()
                {
                    Part = Part,
                    Player = Player,
                    Score = 0
                };

                this.context.PartPlayers.Add(PartPlayer);
                ListPartPlayer.Add(PartPlayer);
            }

            return ListPartPlayer;
        }

        
    }
}
