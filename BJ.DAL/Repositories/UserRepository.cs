using BJ.DAL.Entities;
using BJ.DAL.Interfaces;

namespace BJ.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BJContext context) : base(context)
        {
        }
    }
}
