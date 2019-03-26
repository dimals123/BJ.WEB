using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class UserInGameRepository:GenericRepository<UserInGame>, IUserInGameRepository
    {
        public UserInGameRepository(BJContext context): base(context)
        {

        }  

        public async Task<UserInGame> GetLastGame(string userId)
        {
            var result = await _dbSet
                .Include(x=>x.Game)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Game.IsEnd == false);
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

        public async Task<int> GetPointsByUserIdAndGameId(string userId, Guid gameId)
        {
            var userInGame = await GetByUserIdAndGameId(userId, gameId);
            var points = userInGame.CountPoint;
            return points;
        }
    }
}
