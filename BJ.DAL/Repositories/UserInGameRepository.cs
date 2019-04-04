using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories
{
    public class UserInGameRepository:BaseRepository<UserInGame>, IUserInGameRepository
    {
        public UserInGameRepository(BJContext context): base(context)
        {

        }  

        public async Task<UserInGame> GetUnfinished(string userId)
        {
            var result = await _dbSet
                .Include(x=>x.Game)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Game.IsFinished == false);
            return result;
        }

        public async Task<List<UserInGame>> GetAllByUserId(string userId)
        {
            var games = await _dbSet
                .Include(x => x.Game)
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return games;
        }

        public async Task<UserInGame> GetByUserIdAndGameId(string userId, Guid gameId)
        {
            var userInGame = await _dbSet
                .FirstOrDefaultAsync(x=>x.GameId == gameId && x.UserId == userId);
            return userInGame;
        }

        public async Task<UserInGame> GetLastGame(string userId)
        {
            var response = await _dbSet.Include(x => x.Game).OrderByDescending(x => x.CreationAt).FirstOrDefaultAsync(x => x.UserId == userId);
            return response;
        }

    }
}
