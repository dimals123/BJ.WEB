using System;
using System.Threading.Tasks;
using ViewModels.GameViews;

namespace BJ.BLL.Interfaces
{
    public interface IGameService
    {
        Task<StartGameResultView> StartGame(StartGameView startGameView);
        Task<StartGameResultView> GetCards(GetCardsGameView getCardsGameView);
        Task Stop(GetCardsGameView getCardsGameView);
        Task EndGame(GetCardsGameView getCardsGameView);
       
    }
}
