using BJ.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
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
            var result = await _historyService.GetAllGamesByUserId(UserId);
            return Ok(result);
        }

    }
}
