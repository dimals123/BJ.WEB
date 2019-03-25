using System;
using System.Threading.Tasks;
using ViewModels.GameViews;

namespace BJ.BLL.Interfaces
{
    public interface IGameService
    {
        Task<StartGameResponseView> StartGame(StartGameView startGameView);
        Task GetCards(GetCardsGameView getCardsGameView);
        Task Stop(GetCardsGameView getCardsGameView);
        Task<CreateStartGameResponseView> CreateStartGameResultView(GetCardsGameView getCardsGameView);
        Task<CreateStartGameResponseView> CreateStartGameResultView(CreateStartGameView createStartGameView);
        Task<ReturnLastGameIdView> ReturnLastGame(CreateStartGameView createStartGameView);


    }
}
