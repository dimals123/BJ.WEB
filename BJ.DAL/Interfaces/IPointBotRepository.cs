using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IPointBotRepository:IGeneric<PointBot>
    {
        Task<PointBot> GetMax(Guid botId, Guid gameId);
    }
}
