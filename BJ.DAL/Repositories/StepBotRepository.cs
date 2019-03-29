using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories
{
    public class StepBotRepository : BaseRepository<StepBot>, IStepBotRepository
    {
        public StepBotRepository(BJContext context) : base(context)
        {

        }

        public async Task<List<StepBot>> GetAllByBotIdAndGameId(Guid botId, Guid gameId)
        {
            var stepBots = await _dbSet
                .Select(x => x)
                .Where(u => u.BotId == botId && u.GameId == gameId)
                .ToListAsync();
            return stepBots;
        }

        public async Task<List<StepBot>> GetAllByGameId(Guid gameId)
        {
            var response = await _dbSet
                .Include(x => x.Bot)
                .Where(x => x.GameId == gameId)
                .ToListAsync();

            return response;
        }

        
    }
}
