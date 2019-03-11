using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IUserInGameRepository:IGeneric<UserInGame>
    {
        new void Update(UserInGame model);
        Task<UserInGame> Get(Guid gameId, string userId);
    }
}
