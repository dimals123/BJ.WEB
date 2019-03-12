using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BJ.DAL.Repositories
{
    public class BotRepository:GenericRepository<Bot>, IBotRepository
    {
        public BotRepository(BJContext context):base(context)
        {

        }

        public bool IsCard(BotInGame pointBot)
        {
            if (pointBot.CountPoint <= 16)
                return true;
            else return false;
        }

        public async Task<List<Bot>> GetAllBots(List<BotInGame> botInGames)
        {
            var bots = new List<Bot>();
            foreach(var bot in botInGames)
            {
               bots.Add(await _dbSet.FirstOrDefaultAsync(x=>x.Id == bot.BotId));
            }
            return bots;
        }
       
    }
}
