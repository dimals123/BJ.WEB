using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.DAL.Repositories
{
    public class CardRepository:GenericRepository<Card>, ICardRepository
    {
        public CardRepository(BJContext context):base(context)
        {

        }
    }
}
