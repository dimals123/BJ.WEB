using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IStepUserRepository:IGeneric<StepUser>
    {
        Task<List<StepUser>> GetAllByUserIdAndGameId(string userId, Guid gameId);
    }
}
