using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;

namespace BJ.DataAccess.Repositories.EF
{
    public class GameRepository:BaseRepository<Game>, IGameRepository
    {
        public GameRepository(BJContext context): base(context)
        {

        }
   
    }
}
