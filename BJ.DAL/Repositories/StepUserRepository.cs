using BJ.DAL.Entities;
using BJ.DAL.Interfaces;

namespace BJ.DAL.Repositories
{
    public class StepUserRepository:GenericRepository<StepAccount>, IStepUserRepository
    {
        public StepUserRepository(BJContext context):base(context)
        {

        }
    }
}
