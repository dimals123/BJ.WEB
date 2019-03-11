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

        public new async Task Update(Game item)
        {
            _dbSet.Remove(await _dbSet.FindAsync(item.Id));
            await _dbSet.AddAsync(item);
            base.Update(item);

        }
    }
}
