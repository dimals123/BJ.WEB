using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IBotInGameRepository:IGeneric<BotInGame>
    {
        Task<List<BotInGame>> GetAllBotsInGame(Guid gameId);
        Task<BotInGame> GetByGameIdAndBotId(Guid gameId, Guid botId);
        void UpdateRange(List<BotInGame> botInGames);
        Task<List<BotInGame>> GetAllByGameId(Guid gameId);
    }
}
