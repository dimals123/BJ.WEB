using System;
using System.Threading.Tasks;
using ViewModels.GameViews;

namespace BJ.BLL.Interfaces
{
    public interface IGameService
    {
        Task<StartGameResponseView> Start(int countBots, string userId);
        Task GetCards(Guid gameId, string userId);
        Task Stop(Guid gameId, string userId);
        Task<CreateStartGameResponseView> CreateStartGameResultView(Guid gameId, string userId);
        Task<CreateStartGameResponseView> CreateStartGameResultView(string userId);

    }
}
