using BJ.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IBotRepository:IGeneric<Bot>
    {
        bool IsCard(BotInGame pointBot);
        Task<List<Bot>> GetAllBots(List<BotInGame> botInGames);
        Task<List<Bot>> GetRangeByCount(int count);
    }
}
