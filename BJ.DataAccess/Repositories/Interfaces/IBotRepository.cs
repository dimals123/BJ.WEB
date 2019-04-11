using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IBotRepository:IBaseRepository<Bot>
    {
        Task<List<Bot>> GetCount(int count);
    }
}
