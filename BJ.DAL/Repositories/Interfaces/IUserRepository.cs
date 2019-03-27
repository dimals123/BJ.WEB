using BJ.DAL.Entities;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Interfaces
{
    public interface IUserRepository:IGeneric<User>
    {
        Task<User> GetById(string userId);
        Task<string> GetNameById(string userId);
    }

}
