using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories
{
    public class StepUserRepository:GenericRepository<StepUser>, IStepUserRepository
    {
        public StepUserRepository(BJContext context):base(context)
        {

        }

        public async Task<List<StepUser>> GetAllByUserIdAndGameId(string userId, Guid gameId)
        {
            var stepUsers = await _dbSet.Select(x => x)
                .Where(u => u.GameId == gameId && u.UserId == userId)
                .ToListAsync();
            return stepUsers;
        }
    }
}
