using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BJ.DataAccess.Repositories
{
    public class BotInGameRepository:BaseRepository<BotInGame>, IBotInGameRepository
    {
        public BotInGameRepository(BJContext context):base(context)
        {

        }

        public async Task<List<BotInGame>> GetAllByGameId(Guid gameId)
        {
            var botsInGame = await _dbSet
                .Include(x=>x.Bot)
                .Where(u => u.GameId == gameId)
                .ToListAsync();
            return botsInGame;
        }

        public async Task<BotInGame> GetByGameIdAndBotId(Guid gameId, Guid botId)
        {
            var botsInGame = await _dbSet.FirstOrDefaultAsync(x => x.BotId == botId && x.GameId == gameId);
            return botsInGame;
        }

       
    }
}
