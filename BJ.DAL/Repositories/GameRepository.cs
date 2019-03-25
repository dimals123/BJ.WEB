using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class GameRepository:GenericRepository<Game>, IGameRepository
    {
        public GameRepository(BJContext context): base(context)
        {

        }

        public async Task<Game> GetLastGame(UserInGame userInGame)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == userInGame.GameId);
            return result;
        }

       public async Task<List<Game>> GetAllGamesByUserInGames(List<UserInGame> userInGames)
        {
            var games = new List<Game>();
            foreach(var userInGame in userInGames)
            {
                games.Add(await _dbSet.FirstOrDefaultAsync(x => x.Id == userInGame.GameId));
            }
            return games;
        }
    }
}
