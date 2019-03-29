using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IStepUserRepository:IGeneric<StepUser>
    {
        Task<List<StepUser>> GetAllByUserIdAndGameId(string userId, Guid gameId);
    }
}
