using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class CardRepository:GenericRepository<Card>, ICardRepository
    {
        public CardRepository(BJContext context):base(context)
        {

        }

        //public async Task<List<Card>> GetByGameId(Guid gameId)
        //{
        //    var deck = await _dbSet.Select(x => x).Where(x => x.GameId == gameId).ToListAsync();
        //    return deck;
        //}
        
    }
}
