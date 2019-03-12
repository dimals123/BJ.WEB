using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class GameRepository:GenericRepository<Game>, IGameRepository
    {
        public GameRepository(BJContext context): base(context)
        {

        }
    }
}
