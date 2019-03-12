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

        public virtual async Task CreateRange(List<T> items)
        {
            await _dbSet.AddRangeAsync(items);
        }

        public virtual async Task Create(T item)
        {
            await _dbSet.AddAsync(item);
        }

        public virtual void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual void Update(T item)
        {
            _dbSet.Update(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        public virtual void DeleteRange(List<T> items)
        {
            _dbSet.RemoveRange(items);
        }

        public virtual async Task<T> GetFirst()
        {
           return await _dbSet.FirstOrDefaultAsync();
        }
    }
}
