using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System.Collections.Generic;

namespace BJ.DAL.Repositories
{
    public class CardRepository:GenericRepository<Card>, ICardRepository
    {
        public CardRepository(BJContext context):base(context)
        {

        }

        
    }
}
