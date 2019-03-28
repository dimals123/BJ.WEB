using BJ.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Interfaces
{
    public interface ICardRepository:IGeneric<Card>
    {
        Task<List<Card>> GetAllByGameId(Guid gameId);
        Task<List<Card>> CreateDeck(Guid gameId);
    }
}
