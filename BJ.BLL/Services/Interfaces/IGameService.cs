using System;
using System.Threading.Tasks;
using BJ.ViewModels.GameViews;

namespace BJ.BusinessLogic.Services.Interfaces
{
    public interface IGameService
    {
        Task<StartGameResponseView> Start(int countBots, string userId);
        Task GetCards(Guid gameId, string userId);
        Task Stop(Guid gameId, string userId);
        Task<GetDetailsGameResponseView> GetDetails(Guid gameId, string userId);
        Task<GetUnfinishedGameResponseView> GetUnfinished(string userId);
        Task<Guid> GetUnfinishedId(string userId);
    }
}
