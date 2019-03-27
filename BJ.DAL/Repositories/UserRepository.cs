using BJ.DAL.Entities;
using BJ.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BJContext context) : base(context)
        {
        }

        public async Task<User> GetById(string userId)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<string> GetNameById(string userId)
        {
            var user = await GetById(userId);
            var name = user.UserName;
            return name;
        }

    }
}
