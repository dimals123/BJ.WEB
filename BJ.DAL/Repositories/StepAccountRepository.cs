using BJ.DAL.Entities;
using BJ.DAL.Interfaces;

namespace BJ.DAL.Repositories
{
    public class StepAccountRepository:GenericRepository<StepAccount>, IStepAccountRepository
    {
        public StepAccountRepository(BJContext context):base(context)
        {

        }
    }
}
