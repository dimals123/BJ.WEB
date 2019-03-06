using System;
using System.Linq;
using System.Threading.Tasks;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BJ.DAL.Repositories
{
    public class PointBotRepository:GenericRepository<PointBot>, IPointBotRepository
    {
        public PointBotRepository(BJContext context):base(context)
        {

        }

        public async Task<PointBot> GetBotIdMax(Guid botId, Guid gameId)
        {
            var pointsBot = await _dbSet.Select(x=>x).Where(s=>s.BotId == botId && s.GameId == gameId).ToListAsync();
            var pointBot = pointsBot[0];
            for (int i = 0; i < pointsBot.Count - 1; i++) 
            {
                if (pointBot.CountPoint < pointsBot[i + 1].CountPoint) 
                {
                    pointBot = pointsBot[i + 1];
                }
            }
            return pointBot;
        }
    }
}
