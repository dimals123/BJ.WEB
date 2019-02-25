using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IGeneric<T> where T:class
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(T item);
    }
}
