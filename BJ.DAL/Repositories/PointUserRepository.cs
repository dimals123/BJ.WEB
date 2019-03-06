using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class PointUserRepository:GenericRepository<PointUser>, IPointUserRepository
    {
        public PointUserRepository(BJContext context): base(context)
        {

        }
        public async Task<PointUser> GetMax(string userId, Guid gameId)
        {
            var pointsUser = await _dbSet.Select(x => x).Where(s => s.UserId == userId && s.GameId == gameId).ToListAsync();
            var pointUser = pointsUser[0];
            for (int i = 0; i < pointsUser.Count - 1; i++)
            {
                if (pointUser.CountPoint < pointsUser[i + 1].CountPoint)
                {
                    pointUser = pointsUser[i + 1];
                }
            }
            return pointUser;
        }

       
    }
}
