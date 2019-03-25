using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IUserInGameRepository:IGeneric<UserInGame>
    {
        Task<UserInGame> Get(Guid gameId, string userId);
        Task<UserInGame> GetLastGame(string userId);
        Task<List<UserInGame>> GetAllByUserId(string userId);
        Task<UserInGame> GetByUserIdAndGameId(string userId, Guid gameId);
    }
}
