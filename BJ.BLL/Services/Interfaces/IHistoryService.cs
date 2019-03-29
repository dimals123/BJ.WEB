﻿using BJ.ViewModels.HistoryViews;
using System;
using System.Threading.Tasks;

namespace BJ.BusinessLogic.Services.Interfaces
{
    public interface IHistoryService
    {
        Task Clear();
        Task<GetDetailsGameHistoryView> GetDetailsGame(string userId, Guid gameId);
        Task<GetAllGamesByUserIdView> GetUserGames(string userId);
    }
}
