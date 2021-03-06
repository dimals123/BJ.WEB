﻿using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.EF
{
    public class UserInGameRepository:BaseRepository<UserInGame>, IUserInGameRepository
    {
        public UserInGameRepository(BJContext context): base(context)
        {

        }  

        public async Task<UserInGame> GetUnfinished(string userId)
        {
            var response = await _dbSet
                .Include(x=>x.Game)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Game.IsFinished == false);
            return response;
        }

        public async Task<List<UserInGame>> GetAllByUserId(string userId)
        {
            var response = await _dbSet
                .Include(x => x.Game)
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return response;
        }

        public async Task<List<UserInGame>> GetAllFinishedByUserId(string userId)
        {
            var response = await _dbSet
                .Include(x => x.Game)
                .Select(x => x)
                .Where(x => x.UserId == userId && x.Game.IsFinished == true)
                .ToListAsync();
            return response;
        }

        public async Task<UserInGame> GetByUserIdAndGameId(string userId, Guid gameId)
        {
            var response = await _dbSet
                .FirstOrDefaultAsync(x=>x.GameId == gameId && x.UserId == userId);
            return response;
        }

        public async Task<UserInGame> GetLastGame(string userId)
        {
            var response = await _dbSet
                .Include(x => x.Game)
                .OrderByDescending(x => x.CreationAt)
                .FirstOrDefaultAsync(x => x.UserId == userId);
            return response;
        }

    }
}
