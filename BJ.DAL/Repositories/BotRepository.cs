using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BJ.DataAccess.Repositories
{
    public class BotRepository:GenericRepository<Bot>, IBotRepository
    {
        public BotRepository(BJContext context):base(context)
        {

        }

        public async Task<List<Bot>> GetRangeByCount(int count)
        {
            var result = await _dbSet
                .Select(x => x)
                .Take(count)
                .ToListAsync();
            return result;
        }

        public async Task<int> GetCount()
        {
            var result = await _dbSet.CountAsync();
            return result;
        }

        public void CreateAll(List<Bot> bots)
        {
            _dbSet.AddRange(bots);
            _context.SaveChanges();
        }
       
    }
}
