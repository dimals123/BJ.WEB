using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IUserRepository:IGeneric<User>
    {
        Task<User> Get(string userId);
    }

}
