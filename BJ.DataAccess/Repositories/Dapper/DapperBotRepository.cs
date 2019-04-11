using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperBotRepository: DapperBaseRepository<Bot>, IBotRepository
    {
        public DapperBotRepository(IDbConnection connection) : base(connection)
        {

        }

        public async Task<List<Bot>> GetCount(int count)
        {

            string sql = "SELECT Top (@count) * FROM Bots ORDER BY newid();";
            var result = await _connection.QueryAsync<Bot>(sql, new { count });
            return result.AsList();

        }
    }
}
