using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Interfaces
{
    public interface IStepUserRepository:IGeneric<StepUser>
    {
        Task<List<StepUser>> GetAllByUserIdAndGameId(string userId, Guid gameId);
    }
}
