using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BJ.DataAccess.Repositories.EF
{
    public class BotRepository:BaseRepository<Bot>, IBotRepository
    {
        public BotRepository(BJContext context):base(context)
        {

        }

        public async Task<List<Bot>> GetCount(int count)
        {
            var result = await _dbSet
                .Select(x => x)
                .Take(count)
                .ToListAsync();
            return result;
        }
       
    }
}
