using System;
using System.Threading.Tasks;
using ViewModels.GameViews;

namespace BJ.BLL.Interfaces
{
    public interface IGameService
    {
        Task StartGame(StartGameView startGameView);
        Task GetCards(GetCardsGameView getCardsGameView);
        Task Stop(GetCardsGameView getCardsGameView);
        Task EndGame(GetCardsGameView getCardsGameView);
    }
}
