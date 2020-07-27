using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Models
{
    public class Game
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public int NbMin { get; set; }
        public int NbMax { get; set; }

        public string Description { get; set; }

        public string Rules { get; set; }

        public string CoverImage { get; set; }

        public virtual IEnumerable<Part> Parts { get; set; }
    }
}
