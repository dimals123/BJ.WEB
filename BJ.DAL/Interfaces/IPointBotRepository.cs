using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IPointBotRepository:IGeneric<PointBot>
    {
        Task<PointBot> GetBotIdMax(Guid botId, Guid gameId);
    }
}
