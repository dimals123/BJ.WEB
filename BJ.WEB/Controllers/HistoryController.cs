﻿using BJ.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BJ.WEB.Controllers
{
    public class HistoryController:Controller
    {
        private readonly IHistoryService _historyService;
        public HistoryController(IHistoryService _historyService)
        {
            this._historyService = _historyService;
        }

        public async Task<IActionResult> Clear()
        {
            await _historyService.Clear();
            return Ok();
        }

    }
}
