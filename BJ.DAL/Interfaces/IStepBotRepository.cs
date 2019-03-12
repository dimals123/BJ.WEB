using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IStepBotRepository:IGeneric<StepBot>
    {
        Task<List<StepBot>> GetByBotIdAndGameId(Guid botId, Guid gameId);
    }
}
