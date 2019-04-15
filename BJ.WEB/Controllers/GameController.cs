using BJ.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BJ.WEB.Controllers
{
    [Authorize]
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        public async Task<IActionResult> Start([FromQuery]int countBots)
        {
            await _gameService.Start(countBots, UserId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var gameId = await _gameService.GetUnfinishedId(UserId);
            await _gameService.GetCards(gameId, UserId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Stop()
        {
            var gameId = await _gameService.GetUnfinishedId(UserId);
            await _gameService.Stop(gameId, UserId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetUnfinished()
        {
            var result = await _gameService.GetUnfinished(UserId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> IsUnfinished()
        {
            var response = await _gameService.IsUnfinished(UserId);
            return Ok(response);
        }



        [HttpGet]
        public async Task<IActionResult> GetDetails()
        {
            
            var gameId = await _gameService.GetUnfinishedId(UserId);
            var result = await _gameService.GetDetails(gameId, UserId);
            return Ok(result);
        }

    
    }

   
}
