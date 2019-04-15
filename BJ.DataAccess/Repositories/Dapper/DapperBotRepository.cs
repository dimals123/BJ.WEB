using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using System.Data;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperBotRepository : DapperBaseRepository<Bot>, IBotRepository
    {
        public DapperBotRepository(IDbConnection connection) : base(connection)
        {

        }
    }
}
