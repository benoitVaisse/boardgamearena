using BoardGameArena.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Repositories.IRepository
{
    public interface IGameRepository
    {
        public IEnumerable<Game> FindAll();
        public Game Create(Game Game);

        public Game FindOneById(int Id);


        public Game Update(Game GameChange);
    }
}
