using System;
using System.Collections.Generic;
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

        public PointBot GetMax(Guid botId, Guid gameId, List<PointBot> pointsBot)
        {
            var points = pointsBot.Select(x => x).Where(u => u.BotId == botId && u.GameId == gameId).ToList();
            var pointBot = points.FirstOrDefault();
            if (pointBot != null)
            {
                
                for (int i = 0; i < points.Count - 1; i++)
                {
                    if (pointBot.CountPoint < points[i + 1].CountPoint)
                    {
                        pointBot = points[i + 1];
                    }
                }
                return pointBot;
            }
            else return null;
        }
    }
}
