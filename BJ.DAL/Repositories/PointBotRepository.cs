using BJ.DAL.Entities;
using BJ.DAL.Interfaces;

namespace BJ.DAL.Repositories
{
    public class PointBotRepository:GenericRepository<PointBot>, IPointBotRepository
    {
        public PointBotRepository(BJContext context):base(context)
        {

        }
    }
}
