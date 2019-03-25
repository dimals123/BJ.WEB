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
        [HttpPost]
        public async Task<IActionResult> Start([FromBody]StartGameView startGameView)
        {
            var response = await _gameService.StartGame(startGameView);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> GetCards([FromBody] GetCardsGameView getCardsGameView)
        {
            await _gameService.GetCards(getCardsGameView);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Stop([FromBody] GetCardsGameView getCardsGameView)
        {
            await _gameService.Stop(getCardsGameView);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> GetLastGame([FromBody] CreateStartGameView createStartGameView)

        {
            var result = await _gameService.CreateStartGameResultView(createStartGameView);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetGameById([FromBody] GetCardsGameView getCardsGameView)
        {
            var result = await _gameService.CreateStartGameResultView(getCardsGameView);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetGameId([FromBody] CreateStartGameView createStartGameView)

        {
            var result = await _gameService.ReturnLastGame(createStartGameView);
            return Ok(result);
        }
    }

   
}
