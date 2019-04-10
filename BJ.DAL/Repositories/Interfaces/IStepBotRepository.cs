using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IStepBotRepository:IBaseRepository<BotStep>
    {
        Task<List<BotStep>> GetAllByBotIdAndGameId(Guid botId, Guid gameId);
        Task<List<BotStep>> GetAllByGameId(Guid gameId);
    }
}
