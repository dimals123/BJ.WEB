using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BJContext context) : base(context)
        {
        }

        public async Task<User> Get(string userId)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u=>u.Id == userId);
            return user;
        }
    }
}
