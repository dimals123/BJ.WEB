using BJ.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BJ.WEB.Controllers
{
    [Authorize]
    public class HistoryController:BaseController
    {
        private readonly IHistoryService _historyService;
        public HistoryController(IHistoryService _historyService)
        {
            this._historyService = _historyService;
        }
        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            await _historyService.Clear();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserGames()
        {
            var result = await _historyService.GetUserGames(UserId);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetDetailsGame(Guid gameId)
        {
            var response = await _historyService.GetDetailsGame(UserId, gameId);
            return Ok(response);
        }
    }
}
