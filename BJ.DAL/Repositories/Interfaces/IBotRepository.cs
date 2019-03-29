using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IBotRepository:IGeneric<Bot>
    {
        Task<List<Bot>> GetRangeByCount(int count);
        void CreateAll(List<Bot> bots);
        Task<int> GetCount();
    }
}
