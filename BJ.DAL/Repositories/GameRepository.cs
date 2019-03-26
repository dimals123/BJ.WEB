using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class GameRepository:GenericRepository<Game>, IGameRepository
    {
        public GameRepository(BJContext context): base(context)
        {

        }

        public async Task<Game> GetLastGame(string userId)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.IsEnd == false);
            return result;
        }

   
    }
}
