using BJ.DataAccess.Entities;
using BJ.DataAccess.Entities.Enums;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.EF
{
    public class CardRepository:BaseRepository<Card>, ICardRepository
    {
        public CardRepository(BJContext context):base(context)
        {

        }

        public async Task<List<Card>> GetAllByGameId(Guid gameId)
        {
            var deck = await _dbSet.Select(x => x)
                .Where(x => x.GameId == gameId)
                .ToListAsync();
            return deck;
        }
        
    }
}
