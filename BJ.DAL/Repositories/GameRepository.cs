using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories
{
    public class GameRepository:GenericRepository<Game>, IGameRepository
    {
        public GameRepository(BJContext context): base(context)
        {

        }
   
    }
}
