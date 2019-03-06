using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.BLL.Interfaces
{
    public interface IGameService
    {
        Task StartGame(string userId, int countBots);
        Task GetCards(Guid gameId, string userId);
        Task StopCard(Guid gameId, string userId);
    }
}
