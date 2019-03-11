using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;

namespace BJ.DAL.Repositories
{
    public class BotRepository:GenericRepository<Bot>, IBotRepository
    {
        public BotRepository(BJContext context):base(context)
        {

        }

        public bool IsCard(BotInGame pointBot)
        {
            if (pointBot.CountPoint <= 16)
                return true;
            else return false;
        }

       
    }
}
