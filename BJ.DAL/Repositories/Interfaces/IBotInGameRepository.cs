using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IBotInGameRepository:IBaseRepository<BotInGame>
    {
        Task<BotInGame> GetByGameIdAndBotId(Guid gameId, Guid botId);
        Task<List<BotInGame>> GetAllByGameId(Guid gameId);
    }
}
