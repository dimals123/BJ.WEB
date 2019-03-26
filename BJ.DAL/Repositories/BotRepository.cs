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

        public async Task<string> GetNameById(Guid botId)
        {
            var bot = await GetById(botId);
            var name = bot.Name;
            return name;
        }

        public async Task<List<Bot>> GetRangeByCount(int count)
        {
            var bots = new List<Bot>();
            var allBots = await _dbSet.Select(x => x).ToListAsync();
            int countBots = 0;
            foreach (var bot in allBots)
            {
                if (countBots >= count)
                {
                    break;
                }
                bots.Add(bot);
                countBots++;


            }
            return bots;
        }
       
    }
}
