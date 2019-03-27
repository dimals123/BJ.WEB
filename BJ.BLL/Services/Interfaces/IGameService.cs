using System;
using System.Threading.Tasks;
using BJ.ViewModels.GameViews;

namespace BJ.BLL.Services.Interfaces
{
    public interface IGameService
    {
        Task<StartGameResponseView> Start(int countBots, string userId);
        Task GetCards(Guid gameId, string userId);
        Task Stop(Guid gameId, string userId);
        Task<CreateStartGameResponseView> GetGameByGameIdAndUserId(Guid gameId, string userId);
        Task<CreateStartGameResponseView> GetNoEndGame(string userId);

    }
}
