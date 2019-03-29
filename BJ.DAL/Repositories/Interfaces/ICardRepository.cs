using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Interfaces
{
    public interface ICardRepository:IBaseRepository<Card>
    {
        Task<List<Card>> GetAllByGameId(Guid gameId);
        Task<List<Card>> CreateDeck(Guid gameId);
    }
}
