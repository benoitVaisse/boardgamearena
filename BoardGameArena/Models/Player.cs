using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Models
{
    public class Player : IdentityUser<int>
    {

        public string Photo { get; set; }
        public virtual IEnumerable<PartPlayer> PartPlayers { get; set; }

    }
}
