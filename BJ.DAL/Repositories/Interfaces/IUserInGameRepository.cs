using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface IUserInGameRepository:IGeneric<UserInGame>
    {
        Task<UserInGame> GetUnfinished(string userId);
        Task<List<UserInGame>> GetAllByUserId(string userId);
        Task<UserInGame> GetByUserIdAndGameId(string userId, Guid gameId);
    }
}
