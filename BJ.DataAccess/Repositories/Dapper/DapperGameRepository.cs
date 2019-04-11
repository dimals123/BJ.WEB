using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using System.Data;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperGameRepository: DapperBaseRepository<Game>, IGameRepository
    {
        public DapperGameRepository(IDbConnection connection) : base(connection)
        {

        }
    }
}
