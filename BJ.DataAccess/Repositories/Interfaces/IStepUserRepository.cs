using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IStepUserRepository:IBaseRepository<UserStep>
    {
        Task<List<UserStep>> GetAllByUserIdAndGameId(string userId, Guid gameId);
    }
}
