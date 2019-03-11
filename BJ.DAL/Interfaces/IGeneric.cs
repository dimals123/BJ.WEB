using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IGeneric<T> where T:class
    {
        Task<List<T>> GetAll();
        Task<T> GetFirst();
        Task<T> Get(Guid id);
        Task Create(T item);
        void Update(T item);
        void Delete(T item);
        Task CreateRange(List<T> items);
        void DeleteRange(List<T> items);
    }
}
