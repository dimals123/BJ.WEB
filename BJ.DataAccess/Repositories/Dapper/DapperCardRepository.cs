using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperCardRepository: DapperBaseRepository<Card>, ICardRepository
    {
        public DapperCardRepository(IDbConnection connection) :base(connection)
        {

        }

        public async Task<List<Card>> GetAllByGameId(Guid gameId)
        {

            string sql = "SELECT * FROM Cards Where GameId = @gameId;";
            var result = await _connection.QueryAsync<Card>(sql, new { gameId });
            return result.AsList();

        }
    }
}
