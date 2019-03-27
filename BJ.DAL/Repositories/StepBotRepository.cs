using BJ.DAL.Entities;
using BJ.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class StepBotRepository : GenericRepository<StepBot>, IStepBotRepository
    {
        public StepBotRepository(BJContext context) : base(context)
        {

        }

        public async Task<List<StepBot>> GetByBotIdAndGameId(Guid botId, Guid gameId)
        {
            var stepBots = await _dbSet.Select(x => x).Where(u => u.BotId == botId && u.GameId == gameId).ToListAsync();
            return stepBots;
        }



        
    }
}
