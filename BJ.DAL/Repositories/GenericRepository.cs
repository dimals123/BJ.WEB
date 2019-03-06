using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class GenericRepository<T> : IGeneric<T> where T : class
    {
        protected DbContext _context;
        protected DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task CreateRange(List<T> items)
        {
            await _dbSet.AddRangeAsync(items);
        }

        public async Task Create(T item)
        {
            await _dbSet.AddAsync(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public async Task<T> Get(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void DeleteRange(List<T> items)
        {
            _dbSet.RemoveRange(items);
        }

        public async Task<T> GetFirst()
        {
           return await _dbSet.FirstOrDefaultAsync();
        }
    }
}
