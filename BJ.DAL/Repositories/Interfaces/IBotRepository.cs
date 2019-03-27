using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Interfaces
{
    public interface IBotRepository:IGeneric<Bot>
    {
        Task<List<Bot>> GetRangeByCount(int count);
        Task<string> GetNameById(Guid botId);
    }
}
