using BJ.DAL.Entities;
using BJ.DAL.Interfaces;

namespace BJ.DAL.Repositories
{
    public class StepBotRepository:GenericRepository<StepBot>, IStepBotRepository
    {
        public StepBotRepository(BJContext context):base(context)
        {

        }
    }
}
