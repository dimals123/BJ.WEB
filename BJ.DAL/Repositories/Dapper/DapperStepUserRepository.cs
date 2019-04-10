using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperStepUserRepository : DapperBaseRepository<UserStep>, IStepUserRepository
    {
        public DapperStepUserRepository(IDbConnection connection) : base(connection)
        {

        }

        public async Task<List<UserStep>> GetAllByUserIdAndGameId(string userId, Guid gameId)
        {

            string sql = "SELECT * FROM UserSteps Where UserId = @userId And GameId = @gameId;";
            var result = await _connection.QueryAsync<UserStep>(sql, new { userId, gameId });
            return result.AsList();


        }
    }
}
