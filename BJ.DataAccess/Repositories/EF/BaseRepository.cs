using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.EF
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task Create(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task CreateRange(List<T> items)
        {
            await _dbSet.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(T item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteRange(List<T> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(T item)
        {
            _dbSet.Update(item);
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateRange(List<T> items)
        {
            _dbSet.UpdateRange(items);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<int> GetCount()
        {
            var result = await _dbSet.CountAsync();
            return result;
        }

        public async Task<List<T>> GetCount(int count)
        {
            var result = await _dbSet
                .Select(x => x)
                .Take(count)
                .ToListAsync();
            return result;
        }
    }
}
