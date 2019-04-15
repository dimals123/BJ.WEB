using BJ.ViewModels.HistoryViews;
using System;
using System.Threading.Tasks;

namespace BJ.BusinessLogic.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<GetDetailsGameHistoryView> GetDetailsGame(string userId, Guid gameId);
        Task<GetUserGamesHistoryView> GetUserGames(string userId);
    }
}
