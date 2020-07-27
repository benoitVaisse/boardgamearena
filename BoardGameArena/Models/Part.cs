using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Models
{
    public class Part
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int GameId { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsFinish { get; set; }

        public virtual IEnumerable<PartPlayer> PartPlayers { get; set; }

        public virtual Game Game { get; set; }
    }
}
