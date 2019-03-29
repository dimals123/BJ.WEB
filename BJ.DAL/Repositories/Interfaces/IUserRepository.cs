using BJ.DataAccess.Entities;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository:IGeneric<User>
    {
        Task<User> GetById(string userId);
    }

}
