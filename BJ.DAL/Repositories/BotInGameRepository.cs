using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MoreLinq.Extensions;

namespace BJ.DAL.Repositories
{
    public class BotInGameRepository:GenericRepository<BotInGame>, IBotInGameRepository
    {
        public BotInGameRepository(BJContext context):base(context)
        {

        }

        public async Task<List<BotInGame>> GetAllBots(Guid gameId)
        {
            var botsInGame = await _dbSet.Select(x => x).Where(u => u.GameId == gameId).ToListAsync();
            return botsInGame;
        }
    }
}
