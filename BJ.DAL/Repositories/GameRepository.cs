using BJ.DAL.Entities;
using BJ.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
