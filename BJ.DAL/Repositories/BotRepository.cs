using BJ.DAL.Entities;
using BJ.DAL.Interfaces;

namespace BJ.DAL.Repositories
{
    public class BotRepository:GenericRepository<Bot>, IBotRepository
    {
        public BotRepository(BJContext context):base(context)
        {

        }
    }
}
