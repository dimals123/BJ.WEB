using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IPointUserRepository:IGeneric<PointUser>
    {
        Task<PointUser> GetMax(string userId, Guid gameId);
    }
}
