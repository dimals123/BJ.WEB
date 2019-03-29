using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T:class
    {
        Task<List<T>> GetAll();
        Task<T> GetFirst();
        Task<T> GetById(Guid id);
        Task Create(T item);
        Task CreateRange(List<T> items);
        Task Update(T item);
        Task UpdateRange(List<T> item);
        Task Delete(T item);     
        Task DeleteRange(List<T> items);
    }
}
