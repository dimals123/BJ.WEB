﻿using BJ.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BJ.WEB.Controllers
{
    [Authorize]
    public class GameController:BaseController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet]
        public async Task<IActionResult> Start([FromBody]int countBots)
        {
            var response = await _gameService.Start(countBots, UserId);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetCards([FromBody]Guid gameId)
        {
            await _gameService.GetCards(gameId, UserId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Stop([FromBody]Guid gameId)
        {
            await _gameService.Stop(gameId, UserId);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetLastGame()
        {
            var result = await _gameService.GetUnfinished(UserId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetGameById([FromQuery]Guid gameId)
        {      
            var result = await _gameService.GetDetails(gameId, UserId);
            return Ok(result);
        }

    
    }

   
}
