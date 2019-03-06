using BJ.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await _gameService.StartGame(startGameView);
            return Ok();
        }
    }

   
}
