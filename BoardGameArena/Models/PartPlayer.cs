using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Models
{
    public class PartPlayer
    {

        public int Id { get; set; }

        public int PartId { get; set; }
        public virtual Part Part { get; set; }

        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public int Score { get; set; }
    }
}