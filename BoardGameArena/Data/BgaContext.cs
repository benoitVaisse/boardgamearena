using BoardGameArena.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoardGameArena.ViewModels;

namespace BoardGameArena.Data
{
    public class BgaContext : IdentityDbContext<Player, AppRole, int>
    {
        public BgaContext(DbContextOptions<BgaContext> options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartPlayer> PartPlayers { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options
        //        .UseLazyLoadingProxies()
        //        .UseSqlServer(myConnectionString);
        //}
    }
}
