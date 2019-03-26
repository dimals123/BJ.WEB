using BJ.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Start([FromBody]int countBots)
        {
            var userId = GetUserId();
            var response = await _gameService.Start(countBots, userId);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> GetCards([FromBody]Guid gameId)
        {
            var userId = GetUserId();
            await _gameService.GetCards(gameId, userId);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Stop([FromBody]Guid gameId)
        {
            var userId = GetUserId();
            await _gameService.Stop(gameId, userId);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> GetLastGame()
        {
            var userId = GetUserId();
            var result = await _gameService.CreateStartGameResultView(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetGameById([FromBody]Guid gameId)
        {
            var userId = GetUserId();
            var result = await _gameService.CreateStartGameResultView(gameId, userId);
            return Ok(result);
        }

        private string GetUserId()
        {
            var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }




    }

   
}
