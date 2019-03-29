using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IStepBotRepository:IBaseRepository<StepBot>
    {
        Task<List<StepBot>> GetAllByBotIdAndGameId(Guid botId, Guid gameId);
        Task<List<StepBot>> GetAllByGameId(Guid gameId);
    }
}
