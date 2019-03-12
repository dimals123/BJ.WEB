using BJ.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModels.GameViews;

namespace BJ.WEB.Controllers
{
    [Authorize]
    public class GameController:Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Start([FromBody]StartGameView startGameView)
        {
            var result = await _gameService.StartGame(startGameView);
            return Ok(result);
        }
        public async Task<IActionResult> GetCards([FromBody] GetCardsGameView getCardsGameView)
        {
            var result = await _gameService.GetCards(getCardsGameView);
            return Ok(result);
        }
        public async Task<IActionResult> Stop([FromBody] GetCardsGameView getCardsGameView)
        {
            var result = await _gameService.Stop(getCardsGameView);
            return Ok(result);
        }
    }

   
}
