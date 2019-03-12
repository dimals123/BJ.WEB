using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class UserInGameRepository:GenericRepository<UserInGame>, IUserInGameRepository
    {
        public UserInGameRepository(BJContext context): base(context)
        {

        }  

        public async Task<UserInGame> Get(Guid gameId, string userId)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.GameId == gameId && x.UserId == userId);
            return result;
        }

       
    }
}
