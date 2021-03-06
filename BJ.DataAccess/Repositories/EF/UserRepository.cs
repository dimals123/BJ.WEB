﻿using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.EF
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BJContext context) : base(context)
        {
        }

        public async Task<User> GetById(string userId)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }


    }
}
