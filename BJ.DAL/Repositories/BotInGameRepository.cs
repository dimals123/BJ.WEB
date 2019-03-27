using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJ.DAL.Entities;
using BJ.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BJ.DAL.Repositories
{
    public class BotInGameRepository:GenericRepository<BotInGame>, IBotInGameRepository
    {
        public BotInGameRepository(BJContext context):base(context)
        {

        }

        public async Task<List<BotInGame>> GetAllByGameId(Guid gameId)
        {
            var botsInGame = await _dbSet.Include(x=>x.Bot).Where(u => u.GameId == gameId).ToListAsync();
            return botsInGame;
        }

        public async Task<BotInGame> GetByGameIdAndBotId(Guid gameId, Guid botId)
        {
            var botsInGame = await _dbSet.FirstOrDefaultAsync(x => x.BotId == botId && x.GameId == gameId);
            return botsInGame;
        }

       
    }
}
