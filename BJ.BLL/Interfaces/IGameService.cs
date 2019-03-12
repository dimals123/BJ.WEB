using System;
using System.Threading.Tasks;
using ViewModels.GameViews;

namespace BJ.BLL.Interfaces
{
    public interface IGameService
    {
        Task<StartGameResponseView> StartGame(StartGameView startGameView);
        Task<StartGameResponseView> GetCards(GetCardsGameView getCardsGameView);
        Task<StartGameResponseView> Stop(GetCardsGameView getCardsGameView);
       
    }
}
